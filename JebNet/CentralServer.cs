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
            Context context = CentralServer.Server.RequestLinkedList.First(c => c.RequestContext.Url.Contains("crafts"));
            if (null != context)
            {
                CentralServer.Server.RequestLinkedList.Remove(context);
                using (HttpListenerResponse response = context.HttpListenerResponse)
                using (Stream output = response.OutputStream)
                {

                    try
                    {
                        Logger.Log("Update: processing crafts context.");

                        var vesselList = _vesselIdentityList.ToArray();
                        foreach (var vessel in vesselList)
                        {
                            if (vessel == null)
                            {
                                Logger.Log("NULL");
                            }
                            else
                            {
                                Logger.Log($"Id {vessel.Id} Name {vessel.Name}");
                            }
                        }
                        Logger.Log($"Count: {vesselList.Count()}.");
                        var jsonDebug = JsonUtility.ToJson(vesselList.FirstOrDefault());
                        Logger.Log("{0}", jsonDebug);

                        var serialisedVessel = JsonHelper.arrayToJson<VesselIdentity>(vesselList);
                        Logger.Log("Update: Replying with '{0}'.", serialisedVessel);

                        // Construct a response.
                        byte[] buffer = Encoding.UTF8.GetBytes(serialisedVessel);
                        response.ContentLength64 = buffer.Length;
                        response.StatusCode = 200;

                        output.Write(buffer, 0, buffer.Length);

                        Logger.Log("Update: processing context complete.");
                    }
                    catch (Exception ex)
                    {
                        Logger.Log("Update: Exception caught.");
                        Logger.Log(ex.Message);
                        Logger.Log(ex.StackTrace);
                        byte[] buffer = Encoding.UTF8.GetBytes(ex.Message);
                        response.ContentLength64 = buffer.Length;
                        response.StatusCode = 500;

                        output.Write(buffer, 0, buffer.Length);
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
