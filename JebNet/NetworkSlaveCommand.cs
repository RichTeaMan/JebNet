using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace JebNet
{
    public class NetworkSlaveCommand : PartModule
    {
        /// <summary>
        /// Called once when the part is active in the game (ie, started on the launchpad).
        /// </summary>
        public override void OnStart(StartState state)
        {
            log("Network slave command module on start");
            base.OnStart(state);
        }

        public override void OnActive()
        {
            log("Network slave command module on active");
            base.OnActive();
        }

        public override void OnFixedUpdate()
        {
            log("Network slave command module on fixed update");

            base.OnFixedUpdate();

        }

        /// <summary>
        /// Called frequently during play (every tick?).
        /// </summary>
        public void Update()
        {

            log("Network slave command module update");
        }

        private void log(string message, params object[] args)
        {
            string resolvedMessage = string.Format(message, args);

            string datedResolvedMessage = string.Format("[{0}] - {1}", DateTime.Now, resolvedMessage);

            using (StreamWriter file = new StreamWriter(@"D:\Games\Steam\SteamApps\common\Kerbal Space Program\KSP_Data\Plugins\log.txt", true))
            {
                file.WriteLine(datedResolvedMessage);
            }
        }

    }

}
