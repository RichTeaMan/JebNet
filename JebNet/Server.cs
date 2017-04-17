using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace JebNet
{
    /// <summary>
    /// Lightweight server that batches contexts.
    /// </summary>
    public class Server
    {
        /// <summary>
        /// Port the server is listening on.
        /// </summary>
        public int Port { get; private set; }

        private object requestStackLock = new object();

        private Stack<Context> requestStack = new Stack<Context>();

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
                    log("Attempting to start server on port {0}.", Port);
                    HttpListener = new HttpListener();
                    HttpListener.Prefixes.Add(string.Format("http://localhost:{0}/", Port));
                    HttpListener.Start();
                    HttpListener.BeginGetContext(new AsyncCallback(ListenerCallback), this);


                    log("Server started.");

                }
                catch (Exception ex)
                {
                    log("Failed to start server.");
                    log(ex.Message);
                    log(ex.StackTrace);
                }
            }
        }

        /// <summary>
        /// Stops the server. Dies nothing if the server is already stopped.
        /// </summary>
        public void Stop()
        {
            log("Stopping server.");
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
                requestStack.Push(context);
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
        public Context FetchContext()
        {
            Context context = null;
            lock (requestStackLock)
            {
                if (requestStack.Count > 0)
                {
                    context = requestStack.Pop();
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
                return requestStack.Count;
            }
        }

        /// <summary>
        /// Callback for async server. Adds context to stack and readies server for another exchange.
        /// </summary>
        /// <param name="result"></param>
        private static void ListenerCallback(IAsyncResult result)
        {
            log("Begin listener callback.");
            Server server = (Server)result.AsyncState;
            HttpListener listener = server.HttpListener;
            try
            {
                // Call EndGetContext to complete the asynchronous operation.
                HttpListenerContext httpListenerContext = listener.EndGetContext(result);
                HttpListenerRequest request = httpListenerContext.Request;

                // Obtain a response object.
                HttpListenerResponse response = httpListenerContext.Response;

                string body = null;
                using (StreamReader reader = new StreamReader(request.InputStream))
                {
                    body = reader.ReadToEnd();
                }

                ContextRequest contextRequest = new ContextRequest(request.HttpMethod, body, request.RawUrl);

                Context context = new Context();
                context.RequestContext = contextRequest;
                context.HttpListenerResponse = response;

                server.AddContext(context);
            }
            finally
            {
                listener.BeginGetContext(new AsyncCallback(ListenerCallback), server);
            }
            log("Complete listener callback.");

        }

        /// <summary>
        /// Logs.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        private static void log(string message, params object[] args)
        {
            string resolvedMessage = string.Format(message, args);

            string datedResolvedMessage = string.Format("[{0}] - {1}", DateTime.Now, resolvedMessage);

            using (StreamWriter file = new StreamWriter(@"D:\Games\Steam\SteamApps\common\Kerbal Space Program\KSP_Data\Plugins\log.txt", true))
            {
                file.WriteLine(datedResolvedMessage);
            }
        }

    }

}
