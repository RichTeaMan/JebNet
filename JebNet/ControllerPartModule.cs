using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JebNet
{
    public class ControllerPartModule : PartModule
    {
        /// <summary>
        /// Called when the part is started by Unity.
        /// </summary>
        public override void OnStart(StartState state)
        {
            print("part module started");
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

        }

    }
}
