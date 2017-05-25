using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JebNet.Controller.Integration.Domain
{
    [Serializable]
    public class Vessel
    {
        public double Acceleration_X;
        public double Acceleration_Y;
        public double Acceleration_Z;

        public double AccelerationImmediate_X;
        public double AccelerationImmediate_Y;
        public double AccelerationImmediate_Z;

        public double Altitude;
        public int CurrentStage;
        public bool DirectSunlight;
        public double DistanceToSun;

        public double East_X;
        public double East_Y;
        public double East_Z;

        public double North_X;
        public double North_Y;
        public double North_Z;

        public double GeeForce;
        public double GeeForceImmediate;

        public float HeightFromSurface;
        public float HeightFromTerrain;
        public double HorizontalSrfSpeed;
        public double IndicatedAirSpeed;
        public bool Landed;
        public string LandedAt;
        public string LandedAtLast;

        public double LastUT;

        public double LastVel_X;
        public double LastVel_Y;
        public double LastVel_Z;

        public double Latitude;
        public double Longitude;
        public double Mach;
        public double MissionTime;

        public double OrbitVelocity_X;
        public double OrbitVelocity_Y;
        public double OrbitVelocity_Z;

        public double RadarAltitude;
        public double Speed;
        public double TerrainAltitude;

        public double TerrainNormal_X;
        public double TerrainNormal_Y;
        public double TerrainNormal_Z;

        public double TotalMass;

        public double Up_X;
        public double Up_Y;
        public double Up_Z;

        public double UpAxis_X;
        public double UpAxis_Y;
        public double UpAxis_Z;

        public double VelocityD_X;
        public double VelocityD_Y;
        public double VelocityD_Z;

        public double VerticalSpeed;
        public string VesselName;

        public string SituationString;

        public bool ControlState_GearDown;
        public bool ControlState_GearUp;
        public bool ControlState_HeadLight;
        public float ControlState_MainThrottle;
        public float ControlState_Pitch;
        public float ControlState_PitchTrim;
        public float ControlState_Roll;
        public float ControlState_RollTrim;
        public float ControlState_WheelSteer;
        public float ControlState_WheelSteerTrim;
        public float ControlState_WheelThrottle;
        public float ControlState_WheelThrottleTrim;
        public float ControlState_X;
        public float ControlState_Y;
        public float ControlState_Yaw;
        public float ControlState_YawTrim;
        public float ControlState_Z;


    }
}
