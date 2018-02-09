using JebNet.Domain;
using JebNet.Domain.Mapper;
using KSP.UI.Screens;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using UnityEngine;

namespace JebNet.Server
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
        /// Control state from the network connection.
        /// </summary>
        private ControlState controlState;

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

                log("Attaching fly by wire callback.");

                vessel.OnFlyByWire += new FlightInputCallback(OnFlyByWire);

                log("Completed attaching fly by wire callback.");

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

            log("Unattaching fly by wire callback.");

            vessel.OnFlyByWire -= new FlightInputCallback(OnFlyByWire);

            log("Completed unattaching fly by wire callback.");
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
                using (HttpListenerResponse response = context.HttpListenerResponse)
                using (Stream output = response.OutputStream)
                {

                    try
                    {
                        log("Update: processing context.");
                        if (context.RequestContext.Method == "POST")
                        {
                            log("POST recieved: '{0}'", context.RequestContext.Body);
                            var requestControlState = JsonUtility.FromJson<Domain.ControlState>(context.RequestContext.Body);
                            log("Map complete.");

                            controlState = requestControlState;
                        }

                        var domainVessel = vesselMapper.Map(vessel);

                        var serialisedVessel = JsonUtility.ToJson(domainVessel);
                        log("Update: Replying with '{0}'.", serialisedVessel);

                        // Construct a response.
                        byte[] buffer = Encoding.UTF8.GetBytes(serialisedVessel);
                        response.ContentLength64 = buffer.Length;
                        response.StatusCode = 200;

                        output.Write(buffer, 0, buffer.Length);

                        log("Update: processing context complete.");
                    }
                    catch (Exception ex)
                    {
                        log("Update: Exception caught.");
                        log(ex.Message);
                        log(ex.StackTrace);
                        byte[] buffer = Encoding.UTF8.GetBytes(ex.Message);
                        response.ContentLength64 = buffer.Length;
                        response.StatusCode = 500;

                        output.Write(buffer, 0, buffer.Length);
                    }
                }
            }
        }

        public override void OnInactive()
        {
            base.OnInactive();
        }

        private void OnFlyByWire(FlightCtrlState vesselFlightControlState)
        {
            log("OnFlyByWire: Started");

            vesselFlightControlState.gearDown= controlState.GearDown;
            vesselFlightControlState.gearUp = controlState.GearUp;
            vesselFlightControlState.headlight = controlState.HeadLight;
            vesselFlightControlState.mainThrottle = controlState.MainThrottle;
            vesselFlightControlState.pitch = controlState.Pitch;
            vesselFlightControlState.pitchTrim = controlState.PitchTrim;
            vesselFlightControlState.roll = controlState.Roll;
            vesselFlightControlState.rollTrim = controlState.RollTrim;
            vesselFlightControlState.wheelSteer = controlState.WheelSteer;
            vesselFlightControlState.wheelSteerTrim = controlState.WheelSteerTrim;
            vesselFlightControlState.wheelThrottle = controlState.WheelThrottle;
            vesselFlightControlState.wheelThrottleTrim = controlState.WheelThrottleTrim;
            vesselFlightControlState.X = controlState.X;
            vesselFlightControlState.Y = controlState.Y;
            vesselFlightControlState.yaw = controlState.Yaw;
            vesselFlightControlState.yawTrim = controlState.YawTrim;
            vesselFlightControlState.Z = controlState.Z;

            if (controlState.CurrentStage > vessel.currentStage)
            {
                StageManager.ActivateNextStage();
            }

            log("OnFlyByWire: Ended");
        }

        private static void log(string message, params object[] args)
        {
            try
            {
                string resolvedMessage = string.Format(message, args);

                string datedResolvedMessage = string.Format("[{0}] - {1}", DateTime.Now, resolvedMessage);

                using (StreamWriter file = new StreamWriter(@"D:\Games\Steam\SteamApps\common\Kerbal Space Program\GameData\Squad\Plugins\log.txt", true))
                {
                    file.WriteLine(datedResolvedMessage);
                }
            }
            catch {
                // logging failed, do nothing.
            }
        }

    }

}
