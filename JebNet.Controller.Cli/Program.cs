using JebNet.Controller.Domain;
using JebNet.Controller.Domain.Service;
using JetNet.Controller.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JebNet.Controller.Cli
{
    class Program
    {
        private const int WAIT_PERIOD = 100;

        private static ControllerService controllerService = new ControllerService();

        static void Main(string[] args)
        {
            Console.WriteLine("Press enter to launch vehicle.");
            Console.ReadLine();

            PidController throttleController = new PidController()
            {
                Kp = 0.004,
                Ki = 0.000,
                Kd = 0.2,
                MinOutput = -1.0,
                MaxOutput = 1.0
            };

            Vessel vessel = controllerService.FetchVesselStatus();
            double throttle = 0;

            {
                var controlState = new ControlState(vessel.ControlState);
                controlState.StageVessel();
                controlState.MainThrottle = (float)throttle;
                vessel = controllerService.SendVessel(controlState);
            }
            while (true)
            {

                throttle += throttleController.control(1000, vessel.Altitude, WAIT_PERIOD);
                throttle = Math.Max(0.0, throttle);
                throttle = Math.Min(1.0, throttle);

                var loopControlState = new ControlState(vessel.ControlState);
                loopControlState.MainThrottle = (float)throttle;

                Console.WriteLine("Throttle at {0}%.", throttle * 100);

                vessel = controllerService.SendVessel(loopControlState);

                Thread.Sleep(WAIT_PERIOD);
            }

        }
    }
}
