using KSP.UI.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JebNet.Server.Service
{
    public class VesselControlService
    {

        /// <summary>
        /// Changes the internal vessel to match the given domain vessel.
        /// </summary>
        public void TransformVessel(Domain.Vessel domainVessel, Vessel vessel)
        {
            if (domainVessel.CurrentStage > vessel.currentStage)
            {
                StageManager.ActivateNextStage();
            }
        }
    }
}
