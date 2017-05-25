using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JebNet.Controller.Domain.Mapper
{
    public class VesselMapper
    {
        private VectorMapper vectorMapper = new VectorMapper();

        private ControlStateMapper controlStateMapper = new ControlStateMapper();

        public Vessel MapToDomain(JebNet.Controller.Integration.Domain.Vessel vessel)
        {
            Vessel domainVessel = new Vessel();

            domainVessel.Acceleration = vectorMapper.MapToDomain(vessel.Acceleration_X, vessel.Acceleration_Y, vessel.Acceleration_Z );
            domainVessel.AccelerationImmediate = vectorMapper.MapToDomain(vessel.AccelerationImmediate_X, vessel.AccelerationImmediate_Y, vessel.AccelerationImmediate_Z);
            domainVessel.Altitude = vessel.Altitude;
            domainVessel.CurrentStage = vessel.CurrentStage;
            domainVessel.DirectSunlight = vessel.DirectSunlight;
            domainVessel.DistanceToSun = vessel.DistanceToSun;

            domainVessel.East = vectorMapper.MapToDomain(vessel.East_X, vessel.East_Y, vessel.East_Z);
            domainVessel.North = vectorMapper.MapToDomain(vessel.North_X, vessel.North_Y, vessel.North_Z);

            domainVessel.GeeForce = vessel.GeeForce;
            domainVessel.GeeForceImmediate = vessel.GeeForceImmediate;

            domainVessel.HeightFromSurface = vessel.HeightFromSurface;
            domainVessel.HeightFromTerrain = vessel.HeightFromTerrain;
            domainVessel.HorizontalSrfSpeed = vessel.HorizontalSrfSpeed;
            domainVessel.IndicatedAirSpeed = vessel.IndicatedAirSpeed;
            domainVessel.Landed = vessel.Landed;
            domainVessel.LandedAt = vessel.LandedAt;
            domainVessel.LandedAtLast = vessel.LandedAtLast;

            domainVessel.LastUT = vessel.LastUT;
            domainVessel.LastVel = vectorMapper.MapToDomain(vessel.LastVel_X, vessel.LastVel_Y, vessel.LastVel_Z);

            domainVessel.Latitude = vessel.Latitude;
            domainVessel.Longitude = vessel.Longitude;
            domainVessel.Mach = vessel.Mach;
            domainVessel.MissionTime = vessel.MissionTime;
            domainVessel.OrbitVelocity = vectorMapper.MapToDomain(vessel.OrbitVelocity_X, vessel.OrbitVelocity_Y, vessel.OrbitVelocity_Z);
            domainVessel.RadarAltitude = vessel.RadarAltitude;
            domainVessel.Speed = vessel.Speed;
            domainVessel.TerrainAltitude = vessel.TerrainAltitude;
            domainVessel.TerrainNormal = vectorMapper.MapToDomain(vessel.TerrainNormal_X, vessel.TerrainNormal_Y, vessel.TerrainNormal_Z);
            domainVessel.TotalMass = vessel.TotalMass;
            domainVessel.Up = vectorMapper.MapToDomain(vessel.Up_X, vessel.Up_Y, vessel.Up_Z);
            domainVessel.UpAxis = vectorMapper.MapToDomain(vessel.UpAxis_X, vessel.UpAxis_Y, vessel.UpAxis_Z);
            domainVessel.VelocityD = vectorMapper.MapToDomain(vessel.VelocityD_X, vessel.VelocityD_Y, vessel.VelocityD_Z);
            domainVessel.VerticalSpeed = vessel.VerticalSpeed;
            domainVessel.VesselName = vessel.VesselName;

            domainVessel.SituationString = vessel.SituationString;

            domainVessel.ControlState = controlStateMapper.MapToDomain(vessel);

            return domainVessel;
        }

        public JebNet.Controller.Integration.Domain.Vessel MapToIntegration(Vessel vessel)
        {
            JebNet.Controller.Integration.Domain.Vessel integrationVessel = new JebNet.Controller.Integration.Domain.Vessel();

            integrationVessel.Acceleration_X = vessel.Acceleration.x;
            integrationVessel.Acceleration_Y = vessel.Acceleration.y;
            integrationVessel.Acceleration_Z = vessel.Acceleration.z;
            integrationVessel.AccelerationImmediate_X = vessel.AccelerationImmediate.x;
            integrationVessel.AccelerationImmediate_Y = vessel.AccelerationImmediate.y;
            integrationVessel.AccelerationImmediate_Z = vessel.AccelerationImmediate.z;
            integrationVessel.Altitude = vessel.Altitude;
            integrationVessel.CurrentStage = vessel.CurrentStage;
            integrationVessel.DirectSunlight = vessel.DirectSunlight;
            integrationVessel.DistanceToSun = vessel.DistanceToSun;

            integrationVessel.East_X = vessel.East.x;
            integrationVessel.East_Y = vessel.East.y;
            integrationVessel.East_Z = vessel.East.z;
            integrationVessel.North_X = vessel.North.x;
            integrationVessel.North_Y = vessel.North.y;
            integrationVessel.North_Z = vessel.North.z;

            integrationVessel.GeeForce = vessel.GeeForce;
            integrationVessel.GeeForceImmediate = vessel.GeeForceImmediate;

            integrationVessel.HeightFromSurface = vessel.HeightFromSurface;
            integrationVessel.HeightFromTerrain = vessel.HeightFromTerrain;
            integrationVessel.HorizontalSrfSpeed = vessel.HorizontalSrfSpeed;
            integrationVessel.IndicatedAirSpeed = vessel.IndicatedAirSpeed;
            integrationVessel.Landed = vessel.Landed;
            integrationVessel.LandedAt = vessel.LandedAt;
            integrationVessel.LandedAtLast = vessel.LandedAtLast;

            integrationVessel.LastUT = vessel.LastUT;
            integrationVessel.LastVel_X = vessel.LastVel.x;
            integrationVessel.LastVel_Y = vessel.LastVel.y;
            integrationVessel.LastVel_Z = vessel.LastVel.z;

            integrationVessel.Latitude = vessel.Latitude;
            integrationVessel.Longitude = vessel.Longitude;
            integrationVessel.Mach = vessel.Mach;
            integrationVessel.MissionTime = vessel.MissionTime;
            integrationVessel.OrbitVelocity_X = vessel.OrbitVelocity.x;
            integrationVessel.OrbitVelocity_Y = vessel.OrbitVelocity.y;
            integrationVessel.OrbitVelocity_Z = vessel.OrbitVelocity.z;
            integrationVessel.RadarAltitude = vessel.RadarAltitude;
            integrationVessel.Speed = vessel.Speed;
            integrationVessel.TerrainAltitude = vessel.TerrainAltitude;
            integrationVessel.TerrainNormal_X = vessel.TerrainNormal.x;
            integrationVessel.TerrainNormal_Y = vessel.TerrainNormal.y;
            integrationVessel.TerrainNormal_Z = vessel.TerrainNormal.z;
            integrationVessel.TotalMass = vessel.TotalMass;
            integrationVessel.Up_X = vessel.Up.x;
            integrationVessel.Up_Y = vessel.Up.y;
            integrationVessel.Up_Z = vessel.Up.z;
            integrationVessel.UpAxis_X = vessel.UpAxis.x;
            integrationVessel.UpAxis_Y = vessel.UpAxis.y;
            integrationVessel.UpAxis_Z = vessel.UpAxis.z;

            integrationVessel.VelocityD_X = vessel.VelocityD.x;
            integrationVessel.VelocityD_Y = vessel.VelocityD.y;
            integrationVessel.VelocityD_Z = vessel.VelocityD.z;
            integrationVessel.VerticalSpeed = vessel.VerticalSpeed;
            integrationVessel.VesselName = vessel.VesselName;

            integrationVessel.SituationString = vessel.SituationString;


            integrationVessel.ControlState_GearDown = vessel.ControlState.GearDown;
            integrationVessel.ControlState_GearUp = vessel.ControlState.GearUp;
            integrationVessel.ControlState_HeadLight = vessel.ControlState.HeadLight;
            integrationVessel.ControlState_MainThrottle = vessel.ControlState.MainThrottle;
            integrationVessel.ControlState_Pitch = vessel.ControlState.Pitch;
            integrationVessel.ControlState_PitchTrim = vessel.ControlState.PitchTrim;
            integrationVessel.ControlState_Roll = vessel.ControlState.Roll;
            integrationVessel.ControlState_RollTrim = vessel.ControlState.RollTrim;
            integrationVessel.ControlState_WheelSteer = vessel.ControlState.WheelSteer;
            integrationVessel.ControlState_WheelSteerTrim = vessel.ControlState.WheelSteerTrim;
            integrationVessel.ControlState_WheelThrottle = vessel.ControlState.WheelThrottle;
            integrationVessel.ControlState_WheelThrottleTrim = vessel.ControlState.WheelThrottleTrim;
            integrationVessel.ControlState_X = vessel.ControlState.X;
            integrationVessel.ControlState_Y = vessel.ControlState.Y;
            integrationVessel.ControlState_Yaw = vessel.ControlState.Yaw;
            integrationVessel.ControlState_YawTrim = vessel.ControlState.YawTrim;
            integrationVessel.ControlState_Z = vessel.ControlState.Z;


            return integrationVessel;
        }
    }
}
