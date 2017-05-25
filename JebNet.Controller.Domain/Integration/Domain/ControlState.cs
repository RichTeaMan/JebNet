using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JebNet.Controller.Integration.Domain
{
    [Serializable]
    public class ControlState
    {
        public bool GearDown;
        public bool GearUp;
        public bool HeadLight;
        public float MainThrottle;
        public float Pitch;
        public float PitchTrim;
        public float Roll;
        public float RollTrim;
        public float WheelSteer;
        public float WheelSteerTrim;
        public float WheelThrottle;
        public float WheelThrottleTrim;
        public float X;
        public float Y;
        public float Yaw;
        public float YawTrim;
        public float Z;
        
    }
}
