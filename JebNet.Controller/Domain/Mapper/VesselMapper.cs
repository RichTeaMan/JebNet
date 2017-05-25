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

            domainVessel.Acceleration = vectorMapper.MapToDomain(vessel.Acceleration);
            domainVessel.AccelerationImmediate = vectorMapper.MapToDomain(vessel.AccelerationImmediate);
            domainVessel.Altitude = vessel.Altitude;
            domainVessel.CurrentStage = vessel.CurrentStage;
            domainVessel.DirectSunlight = vessel.DirectSunlight;
            domainVessel.DistanceToSun = vessel.DistanceToSun;

            domainVessel.East = vectorMapper.MapToDomain(vessel.East);
            domainVessel.North = vectorMapper.MapToDomain(vessel.North);

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
            domainVessel.LastVel = vectorMapper.MapToDomain(vessel.LastVel);

            domainVessel.Latitude = vessel.Latitude;
            domainVessel.Longitude = vessel.Longitude;
            domainVessel.Mach = vessel.Mach;
            domainVessel.MissionTime = vessel.MissionTime;
            domainVessel.OrbitVelocity = vectorMapper.MapToDomain(vessel.OrbitVelocity);
            domainVessel.RadarAltitude = vessel.RadarAltitude;
            domainVessel.Speed = vessel.Speed;
            domainVessel.TerrainAltitude = vessel.TerrainAltitude;
            domainVessel.TerrainNormal = vectorMapper.MapToDomain(vessel.TerrainNormal);
            domainVessel.TotalMass = vessel.TotalMass;
            domainVessel.Up = vectorMapper.MapToDomain(vessel.Up);
            domainVessel.UpAxis = vectorMapper.MapToDomain(vessel.UpAxis);
            domainVessel.VelocityD = vectorMapper.MapToDomain(vessel.VelocityD);
            domainVessel.VerticalSpeed = vessel.VerticalSpeed;
            domainVessel.VesselName = vessel.VesselName;

            domainVessel.SituationString = vessel.SituationString;

            domainVessel.ControlState = controlStateMapper.MapToDomain(vessel.ControlState);

            return domainVessel;
        }

        public JebNet.Controller.Integration.Domain.Vessel MapToIntegration(Vessel vessel)
        {
            JebNet.Controller.Integration.Domain.Vessel integrationVessel = new JebNet.Controller.Integration.Domain.Vessel();

            integrationVessel.Acceleration = vectorMapper.MapToIntegration(vessel.Acceleration);
            integrationVessel.AccelerationImmediate = vectorMapper.MapToIntegration(vessel.AccelerationImmediate);
            integrationVessel.Altitude = vessel.Altitude;
            integrationVessel.CurrentStage = vessel.CurrentStage;
            integrationVessel.DirectSunlight = vessel.DirectSunlight;
            integrationVessel.DistanceToSun = vessel.DistanceToSun;

            integrationVessel.East = vectorMapper.MapToIntegration(vessel.East);
            integrationVessel.North = vectorMapper.MapToIntegration(vessel.North);

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
            integrationVessel.LastVel = vectorMapper.MapToIntegration(vessel.LastVel);

            integrationVessel.Latitude = vessel.Latitude;
            integrationVessel.Longitude = vessel.Longitude;
            integrationVessel.Mach = vessel.Mach;
            integrationVessel.MissionTime = vessel.MissionTime;
            integrationVessel.OrbitVelocity = vectorMapper.MapToIntegration(vessel.OrbitVelocity);
            integrationVessel.RadarAltitude = vessel.RadarAltitude;
            integrationVessel.Speed = vessel.Speed;
            integrationVessel.TerrainAltitude = vessel.TerrainAltitude;
            integrationVessel.TerrainNormal = vectorMapper.MapToIntegration(vessel.TerrainNormal);
            integrationVessel.TotalMass = vessel.TotalMass;
            integrationVessel.Up = vectorMapper.MapToIntegration(vessel.Up);
            integrationVessel.UpAxis = vectorMapper.MapToIntegration(vessel.UpAxis);
            integrationVessel.VelocityD = vectorMapper.MapToIntegration(vessel.VelocityD);
            integrationVessel.VerticalSpeed = vessel.VerticalSpeed;
            integrationVessel.VesselName = vessel.VesselName;

            integrationVessel.SituationString = vessel.SituationString;

            integrationVessel.ControlState = controlStateMapper.MapToIntegration(vessel.ControlState);

            return integrationVessel;
        }
    }
}
