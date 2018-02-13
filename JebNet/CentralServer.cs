using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace JebNet
{
    [KSPAddon(KSPAddon.Startup.EveryScene, false)]
    public class CentralServer : MonoBehaviour
    {
        /*
         * Called after the scene is loaded.
         */
        void Awake()
        {
            log("Awake");
        }
        
        /*
         * Called next.
         */
        void Start()
        {
            log("started");
        }

        /*
         * Called every frame
         */
        void Update()
        {
            log("Update");
        }

        /*
         * Called at a fixed time interval determined by the physics time step.
         */
        void FixedUpdate()
        {
            log("FixedUpdate");
        }

        /*
         * Called when the game is leaving the scene (or exiting). Perform any clean up work here.
         */
        void OnDestroy()
        {
            log("OnDestroy");
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

            using (StreamWriter file = new StreamWriter(@"JebNetLogMono.txt", true))
            {
                file.WriteLine(datedResolvedMessage);
            }
        }
    }
}
