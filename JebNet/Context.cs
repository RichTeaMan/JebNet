using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace JebNet
{
    public class Context
    {
        public ContextRequest RequestContext { get; set; }

        public HttpListenerResponse HttpListenerResponse { get; set; }
    }
}
