using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JebNet.Domain.Mapper
{
    public class VesselMapper
    {

        public Vessel Map(global::Vessel vessel)
        {
            Vessel domainVessel = new Vessel();

            domainVessel.Acceleration_X = vessel.acceleration.x;
            domainVessel.Acceleration_Y = vessel.acceleration.y;
            domainVessel.Acceleration_Z = vessel.acceleration.z;
            domainVessel.AccelerationImmediate_X = vessel.acceleration_immediate.x;
            domainVessel.AccelerationImmediate_Y = vessel.acceleration_immediate.y;
            domainVessel.AccelerationImmediate_Z = vessel.acceleration_immediate.z;
            domainVessel.Altitude = vessel.altitude;
            domainVessel.CurrentStage = vessel.currentStage;
            domainVessel.DirectSunlight = vessel.directSunlight;
            domainVessel.DistanceToSun = vessel.distanceToSun;

            domainVessel.East_X = vessel.east.x;
            domainVessel.East_Y = vessel.east.y;
            domainVessel.East_Z = vessel.east.z;
            domainVessel.North_X = vessel.north.x;
            domainVessel.North_Y = vessel.north.y;
            domainVessel.North_Z = vessel.north.z;

            domainVessel.GeeForce = vessel.geeForce;
            domainVessel.GeeForceImmediate = vessel.geeForce_immediate;

            domainVessel.HeightFromSurface = vessel.heightFromSurface;
            domainVessel.HeightFromTerrain = vessel.heightFromTerrain;
            domainVessel.HorizontalSrfSpeed = vessel.horizontalSrfSpeed;
            domainVessel.IndicatedAirSpeed = vessel.indicatedAirSpeed;
            domainVessel.Landed = vessel.Landed;
            domainVessel.LandedAt = vessel.landedAt;
            domainVessel.LandedAtLast = vessel.landedAtLast;

            domainVessel.LastUT = vessel.lastUT;
            domainVessel.LastVel_X = vessel.lastVel.x;
            domainVessel.LastVel_Y = vessel.lastVel.y;
            domainVessel.LastVel_Z = vessel.lastVel.z;

            domainVessel.Latitude = vessel.latitude;
            domainVessel.Longitude = vessel.longitude;
            domainVessel.Mach = vessel.mach;
            domainVessel.MissionTime = vessel.missionTime;
            domainVessel.OrbitVelocity_X = vessel.obt_velocity.x;
            domainVessel.OrbitVelocity_Y = vessel.obt_velocity.y;
            domainVessel.OrbitVelocity_Z = vessel.obt_velocity.z;
            domainVessel.RadarAltitude = vessel.radarAltitude;
            domainVessel.Speed = vessel.speed;
            domainVessel.TerrainAltitude = vessel.terrainAltitude;
            domainVessel.TerrainNormal_X = vessel.terrainNormal.x;
            domainVessel.TerrainNormal_Y = vessel.terrainNormal.y;
            domainVessel.TerrainNormal_Z = vessel.terrainNormal.z;
            domainVessel.TotalMass = vessel.totalMass;
            domainVessel.Up_X = vessel.up.x;
            domainVessel.Up_Y = vessel.up.y;
            domainVessel.Up_Z = vessel.up.z;
            domainVessel.UpAxis_X = vessel.upAxis.x;
            domainVessel.UpAxis_Y = vessel.upAxis.y;
            domainVessel.UpAxis_Z = vessel.upAxis.z;

            domainVessel.VelocityD_X = vessel.velocityD.x;
            domainVessel.VelocityD_Y = vessel.velocityD.y;
            domainVessel.VelocityD_Z = vessel.velocityD.z;
            domainVessel.VerticalSpeed = vessel.verticalSpeed;
            domainVessel.VesselName = vessel.vesselName;

            domainVessel.SituationString = vessel.SituationString;


            domainVessel.ControlState_GearDown = vessel.ctrlState.gearDown;
            domainVessel.ControlState_GearUp = vessel.ctrlState.gearUp;
            domainVessel.ControlState_HeadLight = vessel.ctrlState.headlight;
            domainVessel.ControlState_MainThrottle = vessel.ctrlState.mainThrottle;
            domainVessel.ControlState_Pitch = vessel.ctrlState.pitch;
            domainVessel.ControlState_PitchTrim = vessel.ctrlState.pitchTrim;
            domainVessel.ControlState_Roll = vessel.ctrlState.roll;
            domainVessel.ControlState_RollTrim = vessel.ctrlState.rollTrim;
            domainVessel.ControlState_WheelSteer = vessel.ctrlState.wheelSteer;
            domainVessel.ControlState_WheelSteerTrim = vessel.ctrlState.wheelSteerTrim;
            domainVessel.ControlState_WheelThrottle = vessel.ctrlState.wheelThrottle;
            domainVessel.ControlState_WheelThrottleTrim = vessel.ctrlState.wheelThrottleTrim;
            domainVessel.ControlState_X = vessel.ctrlState.X;
            domainVessel.ControlState_Y = vessel.ctrlState.Y;
            domainVessel.ControlState_Yaw = vessel.ctrlState.yaw;
            domainVessel.ControlState_YawTrim = vessel.ctrlState.yawTrim;
            domainVessel.ControlState_Z = vessel.ctrlState.Z;

            return domainVessel;
        }
    }
}
