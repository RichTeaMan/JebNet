using JebNet.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using UnityEngine;

namespace JebNet.Server
{
    [KSPAddon(KSPAddon.Startup.EveryScene, false)]
    public class CentralServer : MonoBehaviour
    {
        /// <summary>
        /// Logger.
        /// </summary>
        private static JebLogger Logger = new JebLogger(typeof(CentralServer));

        /// <summary>
        /// Port the server is listening on.
        /// </summary>
        private const int NETWORK_PORT = 2001;

        private static object _lock = new object();

        private static List<VesselIdentity> _vesselIdentityList = new List<VesselIdentity>();

        public static Server Server { get; private set; }

        /*
         * Called after the scene is loaded.
         */
        void Awake()
        {
        }

        /*
         * Called next.
         */
        void Start()
        {
            if (null == Server)
            {
                Logger.Log("Central server started");
                Server = new Server(NETWORK_PORT);
                Server.Start();
            }
        }

        /*
         * Called every frame
         */
        void Update()
        {
            using (Context context = Server.FetchContext("crafts"))
            {
                if (null != context)
                {

                    try
                    {
                        Logger.Log("Update: processing crafts context.");

                        var vesselList = _vesselIdentityList.ToArray();
                        var serialisedVessel = JsonHelper.arrayToJson(vesselList);
                        Logger.Log("Update: Replying with '{0}'.", serialisedVessel);

                        context.AddOutput(serialisedVessel);

                        Logger.Log("Update: processing context complete.");
                    }
                    catch (Exception ex)
                    {
                        Logger.Log("Update: Exception caught.");
                        Logger.Log(ex.Message);
                        Logger.Log(ex.StackTrace);
                        context.AddOutput(ex.Message);
                        context.ResponseCode = 500;
                    }
                }
            }
        }

        /*
         * Called at a fixed time interval determined by the physics time step.
         */
        void FixedUpdate()
        {
        }

        /*
         * Called when the game is leaving the scene (or exiting). Perform any clean up work here.
         */
        void OnDestroy()
        {
            Logger.Log("OnDestroy");
        }

        public static void AddVesselIdentity(VesselIdentity vesselIdentity)
        {
            lock (_lock)
            {
                _vesselIdentityList.Add(vesselIdentity);
            }
        }

        public static void RemoveVesselIdentity(VesselIdentity vesselIdentity)
        {
            lock (_lock)
            {
                _vesselIdentityList.Remove(vesselIdentity);
            }
        }
    }
}
