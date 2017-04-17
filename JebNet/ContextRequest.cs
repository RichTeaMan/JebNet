using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JebNet
{
    public class ContextRequest
    {
        public string Method { get; private set; }
        public string Body { get; private set; }
        public string Url { get; private set; }

        public ContextRequest(string method, string body, string url)
        {
            Method = method;
            Body = body;
            Url = url;
        }

    }
}
