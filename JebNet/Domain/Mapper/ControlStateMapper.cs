using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JebNet.Domain.Mapper
{
    public class ControlStateMapper
    {
        public ControlState Map(global::FlightCtrlState flightControlState)
        {
            ControlState domainControlState = null;
            if (null != flightControlState)
            {
                domainControlState = new ControlState();

                domainControlState.GearDown = flightControlState.gearDown;
                domainControlState.GearUp = flightControlState.gearUp;
                domainControlState.HeadLight = flightControlState.headlight;
                domainControlState.MainThrottle = flightControlState.mainThrottle;
                domainControlState.Pitch = flightControlState.pitch;
                domainControlState.PitchTrim = flightControlState.pitchTrim;
                domainControlState.Roll = flightControlState.roll;
                domainControlState.RollTrim = flightControlState.rollTrim;
                domainControlState.WheelSteer = flightControlState.wheelSteer;
                domainControlState.WheelSteerTrim = flightControlState.wheelSteerTrim;
                domainControlState.WheelThrottle = flightControlState.wheelThrottle;
                domainControlState.WheelThrottleTrim = flightControlState.wheelThrottleTrim;
                domainControlState.X = flightControlState.X;
                domainControlState.Y = flightControlState.Y;
                domainControlState.Yaw = flightControlState.yaw;
                domainControlState.YawTrim = flightControlState.yawTrim;
                domainControlState.Z = flightControlState.Z;
            }
            return domainControlState;
        }
    }
}
