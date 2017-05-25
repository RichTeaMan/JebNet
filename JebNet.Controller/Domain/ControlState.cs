using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JebNet.Controller.Domain
{
    public class ControlState
    {
        public bool GearDown { get; set; }
        public bool GearUp { get; set; }
        public bool HeadLight { get; set; }
        public float MainThrottle { get; set; }
        public float Pitch { get; set; }
        public float PitchTrim { get; set; }
        public float Roll { get; set; }
        public float RollTrim { get; set; }
        public float WheelSteer { get; set; }
        public float WheelSteerTrim { get; set; }
        public float WheelThrottle { get; set; }
        public float WheelThrottleTrim { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Yaw { get; set; }
        public float YawTrim { get; set; }
        public float Z { get; set; }
        public int CurrentStage { get; set; }

        public ControlState()
        {

        }

        /// <summary>
        /// Clones a control state.
        /// </summary>
        /// <param name="controlState"></param>
        public ControlState(ControlState controlState)
        {
            GearDown = controlState.GearDown;
            GearUp = controlState.GearUp;
            HeadLight = controlState.HeadLight;
            MainThrottle = controlState.MainThrottle;
            Pitch = controlState.Pitch;
            PitchTrim = controlState.PitchTrim;
            Roll = controlState.Roll;
            RollTrim = controlState.RollTrim;
            WheelSteer = controlState.WheelSteer;
            WheelSteerTrim = controlState.WheelSteerTrim;
            WheelThrottle = controlState.WheelThrottle;
            WheelThrottleTrim = controlState.WheelThrottleTrim;
            X = controlState.X;
            Y = controlState.Y;
            Yaw = controlState.Yaw;
            YawTrim = controlState.YawTrim;
            Z = controlState.Z;
            CurrentStage = controlState.CurrentStage;
        }

        /// <summary>
        /// Stages the vessel.
        /// </summary>
        public void StageVessel()
        {
            CurrentStage++;
        }

    }
}
