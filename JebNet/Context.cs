using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using JebNet.Server;

namespace JebNet
{
    public class Context : IDisposable
    {
        public int ResponseCode { get; set; } = 200;

        public ContextRequest ContextRequest { get; private set; }

        private HttpListenerResponse _httpListenerResponse;

        private Server.Server _server;

        private string _output;


        public Context(ContextRequest contextRequest, HttpListenerResponse response, Server.Server server)
        {
            ContextRequest = contextRequest;
            _httpListenerResponse = response;
            _server = server;
        }

        public void AddOutput(string output)
        {
            _output = output;
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    using (HttpListenerResponse response = _httpListenerResponse)
                    using (Stream outputStream = response.OutputStream)
                    {
                        byte[] buffer = new byte[0];
                        if (_output.Any())
                        {
                            buffer = Encoding.UTF8.GetBytes(_output);
                        }
                        response.ContentLength64 = buffer.Length;
                        response.StatusCode = ResponseCode;
                        outputStream.Write(buffer, 0, buffer.Length);
                        Console.WriteLine("sending response");
                    }

                    _server?.RemoveContext(this);
                }

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
