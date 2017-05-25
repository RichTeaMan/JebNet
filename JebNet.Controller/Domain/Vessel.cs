using System;

namespace JebNet.Controller.Domain
{
    public class Vessel
    {
        public Vector Acceleration{ get; set; }
        public Vector AccelerationImmediate{ get; set; }
        public double Altitude{ get; set; }
        public int CurrentStage{ get; set; }
        public bool DirectSunlight{ get; set; }
        public double DistanceToSun{ get; set; }

        public Vector East{ get; set; }
        public Vector North{ get; set; }

        public double GeeForce{ get; set; }
        public double GeeForceImmediate{ get; set; }

        public float HeightFromSurface{ get; set; }
        public float HeightFromTerrain{ get; set; }
        public double HorizontalSrfSpeed{ get; set; }
        public double IndicatedAirSpeed{ get; set; }
        public bool Landed{ get; set; }
        public string LandedAt{ get; set; }
        public string LandedAtLast{ get; set; }

        public double LastUT{ get; set; }
        public Vector LastVel{ get; set; }

        public double Latitude{ get; set; }
        public double Longitude{ get; set; }
        public double Mach{ get; set; }
        public double MissionTime{ get; set; }
        public Vector OrbitVelocity{ get; set; }
        public double RadarAltitude{ get; set; }
        public double Speed{ get; set; }
        public double TerrainAltitude{ get; set; }
        public Vector TerrainNormal{ get; set; }
        public double TotalMass{ get; set; }
        public Vector Up{ get; set; }
        public Vector UpAxis{ get; set; }
        public Vector VelocityD{ get; set; }
        public double VerticalSpeed{ get; set; }
        public string VesselName{ get; set; }

        public string SituationString{ get; set; }

        public ControlState ControlState { get; set; }

        /// <summary>
        /// Stages the vessel.
        /// </summary>
        public void StageVessel()
        {
            CurrentStage++;
        }

    }
}
