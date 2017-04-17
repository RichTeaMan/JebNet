using JebNet.Domain.Mapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using UnityEngine;

namespace JebNet
{
    public class NetworkSlaveCommand : PartModule
    {
        /// <summary>
        /// Port the vessel is listening on.
        /// </summary>
        private const int NETWORK_PORT = 2001;

        /// <summary>
        /// Server to get control messages.
        /// </summary>
        private Server server;

        /// <summary>
        /// Vessel mapper.
        /// </summary>
        private VesselMapper vesselMapper = new VesselMapper();

        /// <summary>
        /// Called once when the part is active in the game (ie, started on the launchpad).
        /// </summary>
        public override void OnStart(StartState state)
        {

            log("Network slave command module on start. State is '{0}'.", state);

            try
            {
                log("Attempting to start server.");
                server = new Server(NETWORK_PORT);
                server.Start();

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
            if (null != server)
            {
                server.Stop();
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

        /// <summary>
        /// Called frequently during play (every tick?).
        /// </summary>
        public void Update()
        {
            
            Context context = server.FetchContext();
            if (null != context)
            {
                log("Update: processing context.");
                HttpListenerResponse response = context.HttpListenerResponse;

                var domainVessel = vesselMapper.Map(vessel);

                var serialisedVessel = JsonUtility.ToJson(domainVessel);

                // Construct a response.
                //string responseString = string.Format("<HTML><BODY> Hello world at {0}!</BODY></HTML>", DateTime.Now);
                byte[] buffer = Encoding.UTF8.GetBytes(serialisedVessel);
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
