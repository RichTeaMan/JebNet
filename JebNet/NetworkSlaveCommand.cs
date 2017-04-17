using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace JebNet
{
    public class NetworkSlaveCommand : PartModule
    {
        /// <summary>
        /// Port the vessel is listening on.
        /// </summary>
        private const int NETWORK_PORT = 2001;

        private object requestStackLock = new object();

        private Stack<Context> requestStack = new Stack<Context>();

        /// <summary>
        /// The HTTP listener.
        /// </summary>
        public HttpListener HttpListener { get; private set; }

        public void AddContext(Context context)
        {
            lock (requestStackLock)
            {
                requestStack.Push(context);
            }
        }

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
        /// Called once when the part is active in the game (ie, started on the launchpad).
        /// </summary>
        public override void OnStart(StartState state)
        {

            log("Network slave command module on start. State is '{0}'.", state);

            try
            {
                log("Attempting to start server.");
                HttpListener = new HttpListener();
                HttpListener.Prefixes.Add(string.Format("http://localhost:{0}/", NETWORK_PORT));
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

            base.OnStart(state);
        }

        public void OnDestroy()
        {
            log("Network slave command on destroy.");
            if (null != HttpListener)
            {
                HttpListener.Stop();
            }
        }

        public override void OnActive()
        {
            log("Network slave command module on active");
            base.OnActive();
        }

        public override void OnFixedUpdate()
        {
            log("Network slave command module on fixed update");

            base.OnFixedUpdate();

        }

        public static void ListenerCallback(IAsyncResult result)
        {
            log("Begin listener callback.");
            NetworkSlaveCommand networkSlaveCommand = (NetworkSlaveCommand)result.AsyncState;
            HttpListener listener = networkSlaveCommand.HttpListener;
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

                networkSlaveCommand.AddContext(context);
            }
            finally
            {
                listener.BeginGetContext(new AsyncCallback(ListenerCallback), networkSlaveCommand);
            }
            log("Complete listener callback.");

        }

        /// <summary>
        /// Called frequently during play (every tick?).
        /// </summary>
        public void Update()
        {

            Context context = FetchContext();
            if (null != context)
            {
                log("Update: processing context.");
                HttpListenerResponse response = context.HttpListenerResponse;
                // Construct a response.
                string responseString = string.Format("<HTML><BODY> Hello world at {0}!</BODY></HTML>", DateTime.Now);
                byte[] buffer = Encoding.UTF8.GetBytes(responseString);
                // Get a response stream and write the response to it.
                response.ContentLength64 = buffer.Length;
                System.IO.Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                // You must close the output stream.
                output.Close();
                response.Close();
                log("Update: processing context complete.");
            }

        }

        public override void OnInactive()
        {
            base.OnInactive();
        }

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
