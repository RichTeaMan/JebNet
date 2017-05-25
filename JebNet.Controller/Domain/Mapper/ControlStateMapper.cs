using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JebNet.Controller.Domain.Mapper
{
    public class ControlStateMapper
    {
        public ControlState MapToDomain(JebNet.Controller.Integration.Domain.Vessel vessel)
        {
            ControlState domainControlState = null;
            if (null != vessel)
            {
                domainControlState = new ControlState();

                domainControlState.GearDown = vessel.ControlState_GearDown;
                domainControlState.GearUp = vessel.ControlState_GearUp;
                domainControlState.HeadLight = vessel.ControlState_HeadLight;
                domainControlState.MainThrottle = vessel.ControlState_MainThrottle;
                domainControlState.Pitch = vessel.ControlState_Pitch;
                domainControlState.PitchTrim = vessel.ControlState_PitchTrim;
                domainControlState.Roll = vessel.ControlState_Roll;
                domainControlState.RollTrim = vessel.ControlState_RollTrim;
                domainControlState.WheelSteer = vessel.ControlState_WheelSteer;
                domainControlState.WheelSteerTrim = vessel.ControlState_WheelSteerTrim;
                domainControlState.WheelThrottle = vessel.ControlState_WheelThrottle;
                domainControlState.WheelThrottleTrim = vessel.ControlState_WheelThrottleTrim;
                domainControlState.X = vessel.ControlState_X;
                domainControlState.Y = vessel.ControlState_Y;
                domainControlState.Yaw = vessel.ControlState_Yaw;
                domainControlState.YawTrim = vessel.ControlState_YawTrim;
                domainControlState.Z = vessel.ControlState_Z;
                domainControlState.CurrentStage = vessel.CurrentStage;
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
                integrationControlState.CurrentStage = controlState.CurrentStage;
            }
            return integrationControlState;
        }

    }
}
