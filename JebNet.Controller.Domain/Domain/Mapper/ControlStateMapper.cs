using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JebNet.Controller.Domain.Mapper
{
    public class ControlStateMapper
    {
        public ControlState MapToDomain(JebNet.Controller.Integration.Domain.ControlState controlState)
        {
            ControlState domainControlState = null;
            if (null != controlState)
            {
                domainControlState = new ControlState();

                domainControlState.GearDown = controlState.GearDown;
                domainControlState.GearUp = controlState.GearUp;
                domainControlState.HeadLight = controlState.HeadLight;
                domainControlState.MainThrottle = controlState.MainThrottle;
                domainControlState.Pitch = controlState.Pitch;
                domainControlState.PitchTrim = controlState.PitchTrim;
                domainControlState.Roll = controlState.Roll;
                domainControlState.RollTrim = controlState.RollTrim;
                domainControlState.WheelSteer = controlState.WheelSteer;
                domainControlState.WheelSteerTrim = controlState.WheelSteerTrim;
                domainControlState.WheelThrottle = controlState.WheelThrottle;
                domainControlState.WheelThrottleTrim = controlState.WheelThrottleTrim;
                domainControlState.X = controlState.X;
                domainControlState.Y = controlState.Y;
                domainControlState.Yaw = controlState.Yaw;
                domainControlState.YawTrim = controlState.YawTrim;
                domainControlState.Z = controlState.Z;
            }
            return domainControlState;
        }

        public JebNet.Controller.Integration.Domain.ControlState MapToIntegration(ControlState controlState)
        {
            JebNet.Controller.Integration.Domain.ControlState integrationControlState = null;
            if (null != controlState)
            {
                integrationControlState = new JebNet.Controller.Integration.Domain.ControlState();

                integrationControlState.GearDown = controlState.GearDown;
                integrationControlState.GearUp = controlState.GearUp;
                integrationControlState.HeadLight = controlState.HeadLight;
                integrationControlState.MainThrottle = controlState.MainThrottle;
                integrationControlState.Pitch = controlState.Pitch;
                integrationControlState.PitchTrim = controlState.PitchTrim;
                integrationControlState.Roll = controlState.Roll;
                integrationControlState.RollTrim = controlState.RollTrim;
                integrationControlState.WheelSteer = controlState.WheelSteer;
                integrationControlState.WheelSteerTrim = controlState.WheelSteerTrim;
                integrationControlState.WheelThrottle = controlState.WheelThrottle;
                integrationControlState.WheelThrottleTrim = controlState.WheelThrottleTrim;
                integrationControlState.X = controlState.X;
                integrationControlState.Y = controlState.Y;
                integrationControlState.Yaw = controlState.Yaw;
                integrationControlState.YawTrim = controlState.YawTrim;
                integrationControlState.Z = controlState.Z;
            }
            return integrationControlState;
        }
    }
}
