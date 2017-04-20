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

                domainControlState.GearDown = true;
                domainControlState.GearUp = true;
                domainControlState.HeadLight = true;
                domainControlState.MainThrottle = 0;
                domainControlState.Pitch = 0;
                domainControlState.PitchTrim = 0;
                domainControlState.Roll = 0;
                domainControlState.RollTrim = 0;
                domainControlState.WheelSteer = 0;
                domainControlState.WheelSteerTrim = 0;
                domainControlState.WheelThrottle = 0;
                domainControlState.WheelThrottleTrim = 0;
                domainControlState.X = 0;
                domainControlState.Y = 0;
                domainControlState.Yaw = 0;
                domainControlState.YawTrim = 0;
                domainControlState.Z = 0;
            }
            return domainControlState;
        }
    }
}
