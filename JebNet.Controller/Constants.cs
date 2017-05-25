using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JebNet.Controller
{
    /// <summary>
    /// Constants for the controller.
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// URL of the server inside the vessel.
        /// </summary>
        public static readonly string SERVER_URL = "http://localhost:2001/";

        /// <summary>
        /// Path of status endpoint.
        /// </summary>
        public static readonly string STATUS_ENDPOINT = "status";

        /// <summary>
        /// Path of command endpoint.
        /// </summary>
        public static readonly string COMMAND_ENDPOINT = "command";
    }
}
