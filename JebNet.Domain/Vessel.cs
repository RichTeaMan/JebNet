using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JebNet
{
    [Serializable]
    public class Vessel
    {
        public Vector Acceleration;
        public Vector AccelerationImmediate;
        public double Altitude;
        public int CurrentStage;
        public bool DirectSunlight;
        public double DistanceToSun;

        public Vector East;
        public Vector North;

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
        public Vector LastVel;

        public double Latitude;
        public double Longitude;
        public double Mach;
        public double MissionTime;
        public Vector OrbitVelocity;
        public double RadarAltitude;
        public double Speed;
        public double TerrainAltitude;
        public Vector TerrainNormal;
        public double TotalMass;
        public Vector Up;
        public Vector UpAxis;
        public Vector VelocityD;
        public double VerticalSpeed;
        public string VesselName;

        public string SituationString;

    }
}
