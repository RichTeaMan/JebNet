using JebNet.Domain;
using JebNet.Domain.Mapper;
using KSP.UI.Screens;
using System;
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
        /// Logger.
        /// </summary>
        private static JebLogger Logger = new JebLogger(typeof(NetworkSlaveCommand));

        public VesselIdentity VesselIdentity { get; private set; }

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

            Logger.Log("Network slave command module on start. State is '{0}'.", state);

            try
            {
                VesselIdentity = new VesselIdentity
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = vessel.name
                };
                CentralServer.AddVesselIdentity(VesselIdentity);

                Logger.Log("Attaching fly by wire callback.");

                vessel.OnFlyByWire += new FlightInputCallback(OnFlyByWire);

                Logger.Log("Completed attaching fly by wire callback.");

            }
            catch (Exception ex)
            {
                Logger.Log("Failed to start server.");
                Logger.Log(ex.Message);
                Logger.Log(ex.StackTrace);
            }

            base.OnStart(state);
        }

        public void OnDestroy()
        {
            Logger.Log("Unattaching fly by wire callback.");

            if (null != vessel)
            {
                vessel.OnFlyByWire -= new FlightInputCallback(OnFlyByWire);
            }
            CentralServer.RemoveVesselIdentity(VesselIdentity);

            Logger.Log("Completed unattaching fly by wire callback.");
        }

        public override void OnActive()
        {
            Logger.Log("Network slave command module on active");
            base.OnActive();
        }

        public override void OnFixedUpdate()
        {
            Logger.Log("Network slave command module on fixed update");

            base.OnFixedUpdate();

        }

        /// <summary>
        /// Called frequently during play (every tick?).
        /// </summary>
        public void Update()
        {

            Context context = CentralServer.Server.RequestLinkedList.First(c => !c.RequestContext.Url.Contains("crafts"));
            if (null != context)
            {
                CentralServer.Server.RequestLinkedList.Remove(context);
                using (HttpListenerResponse response = context.HttpListenerResponse)
                using (Stream output = response.OutputStream)
                {

                    try
                    {
                        Logger.Log("Update: processing context.");
                        if (context.RequestContext.Method == "POST")
                        {
                            Logger.Log("POST recieved: '{0}'", context.RequestContext.Body);
                            var requestControlState = JsonUtility.FromJson<ControlState>(context.RequestContext.Body);
                            Logger.Log("Map complete.");

                            controlState = requestControlState;
                        }

                        var domainVessel = vesselMapper.Map(vessel);

                        var serialisedVessel = JsonUtility.ToJson(domainVessel);
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

        public override void OnInactive()
        {
            base.OnInactive();
        }

        private void OnFlyByWire(FlightCtrlState vesselFlightControlState)
        {
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
        }
    }
}
