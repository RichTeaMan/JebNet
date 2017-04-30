using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JebNet.Domain.Mapper
{
    public class VesselMapper
    {
        private VectorMapper vectorMapper = new VectorMapper();

        private ControlStateMapper controlStateMapper = new ControlStateMapper();


        public Vessel Map(global::Vessel vessel)
        {
            Vessel domainVessel = new Vessel();

            domainVessel.Acceleration = vectorMapper.Map(vessel.acceleration);
            domainVessel.AccelerationImmediate = vectorMapper.Map(vessel.acceleration_immediate);
            domainVessel.Altitude = vessel.altitude;
            domainVessel.CurrentStage = vessel.currentStage;
            domainVessel.DirectSunlight = vessel.directSunlight;
            domainVessel.DistanceToSun = vessel.distanceToSun;

            domainVessel.East = vectorMapper.Map(vessel.east);
            domainVessel.North = vectorMapper.Map(vessel.north);

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
            domainVessel.LastVel = vectorMapper.Map(vessel.lastVel);

            domainVessel.Latitude = vessel.latitude;
            domainVessel.Longitude = vessel.longitude;
            domainVessel.Mach = vessel.mach;
            domainVessel.MissionTime = vessel.missionTime;
            domainVessel.OrbitVelocity = vectorMapper.Map(vessel.obt_velocity);
            domainVessel.RadarAltitude = vessel.radarAltitude;
            domainVessel.Speed = vessel.speed;
            domainVessel.TerrainAltitude = vessel.terrainAltitude;
            domainVessel.TerrainNormal = vectorMapper.Map(vessel.terrainNormal);
            domainVessel.TotalMass = vessel.totalMass;
            domainVessel.Up = vectorMapper.Map(vessel.up);
            domainVessel.UpAxis = vectorMapper.Map(vessel.upAxis);
            domainVessel.VelocityD = vectorMapper.Map(vessel.velocityD);
            domainVessel.VerticalSpeed = vessel.verticalSpeed;
            domainVessel.VesselName = vessel.vesselName;

            domainVessel.SituationString = vessel.SituationString;

            domainVessel.ControlState = controlStateMapper.Map(vessel.ctrlState);

            return domainVessel;
        }
    }
}
