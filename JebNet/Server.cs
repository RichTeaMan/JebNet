using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace JebNet.Server
{
    /// <summary>
    /// Lightweight server that batches contexts.
    /// </summary>
    public class Server
    {
        /// <summary>
        /// Logger.
        /// </summary>
        private static JebLogger Logger = new JebLogger(typeof(CentralServer));

        /// <summary>
        /// Port the server is listening on.
        /// </summary>
        public int Port { get; private set; }

        private object requestStackLock = new object();

        private LinkedList<Context> RequestLinkedList = new LinkedList<Context>();

        /// <summary>
        /// The HTTP listener.
        /// </summary>
        public HttpListener HttpListener { get; private set; }

        /// <summary>
        /// Constructs server to listen on the given port.
        /// </summary>
        /// <param name="port">Port to listen on.</param>
        public Server(int port)
        {
            Port = port;
        }

        /// <summary>
        /// Starts the server. Does nothing if the server is already started.
        /// </summary>
        public void Start()
        {
            if (!(HttpListener != null && HttpListener.IsListening))
            {
                try
                {
                    Logger.Log("Attempting to start server on port {0}.", Port);
                    HttpListener = new HttpListener();
                    HttpListener.Prefixes.Add(string.Format("http://localhost:{0}/", Port));
                    HttpListener.Start();
                    HttpListener.BeginGetContext(new AsyncCallback(ListenerCallback), this);

                    Logger.Log("Server started.");
                }
                catch (Exception ex)
                {
                    Logger.Log("Failed to start server.");
                    Logger.Log(ex.Message);
                    Logger.Log(ex.StackTrace);
                }
            }
        }

        /// <summary>
        /// Stops the server. Dies nothing if the server is already stopped.
        /// </summary>
        public void Stop()
        {
            Logger.Log("Stopping server.");
            if (null != HttpListener)
            {
                HttpListener.Stop();
            }
        }

        /// <summary>
        /// Adds a context to the internal stack. This method is thread safe.
        /// </summary>
        /// <param name="context"></param>
        private void AddContext(Context context)
        {
            lock (requestStackLock)
            {
                RequestLinkedList.AddLast(context);
            }
        }

        /// <summary>
        /// Removes a context to the internal stack. This method is thread safe.
        /// </summary>
        /// <param name="context"></param>
        public void RemoveContext(Context context)
        {
            lock (requestStackLock)
            {
                RequestLinkedList.Remove(context);
            }
        }

        /// <summary>
        /// Fetches and removes the context from the internal stack. This method is thread safe.
        /// 
        /// The caller is expected to deal with the context and close any streams for the connection can be completed.
        /// 
        /// Returns null if there are no contexts in the stack.
        /// </summary>
        /// <returns></returns>
        public Context FetchContext(string pathContains)
        {
            Context context = null;
            lock (requestStackLock)
            {
                foreach(var requestContext in RequestLinkedList)
                {
                    if (requestContext.ContextRequest.Url.Contains(pathContains))
                    {
                        context = requestContext;
                        Logger.Log($"Context requested: {context.ContextRequest.Url}");
                        break;
                    }
                }
            }
            return context;
        }

        /// <summary>
        /// Gets how many contexts are waiting. This method is thread safe.
        /// </summary>
        /// <returns></returns>
        public int FetchContextCount()
        {
            lock (requestStackLock)
            {
                return RequestLinkedList.Count;
            }
        }

        /// <summary>
        /// Callback for async server. Adds context to stack and readies server for another exchange.
        /// </summary>
        /// <param name="result"></param>
        private static void ListenerCallback(IAsyncResult result)
        {
            Logger.Log("Begin listener callback.");
            Server server = (Server)result.AsyncState;
            HttpListener listener = server.HttpListener;
            try
            {
                // Call EndGetContext to complete the asynchronous operation.
                HttpListenerContext httpListenerContext = listener.EndGetContext(result);
                HttpListenerRequest request = httpListenerContext.Request;
                HttpListenerResponse response = httpListenerContext.Response;

                string body = null;
                using (StreamReader reader = new StreamReader(request.InputStream))
                {
                    body = reader.ReadToEnd();
                }

                ContextRequest contextRequest = new ContextRequest(request.HttpMethod, body, request.RawUrl);

                Context context = new Context(contextRequest, response, server);

                Logger.Log($"Request received: {context.ContextRequest.Url}");

                server.AddContext(context);
            }
            finally
            {
                listener.BeginGetContext(new AsyncCallback(ListenerCallback), server);
            }
            Logger.Log("Complete listener callback.");

        }

    }

}
