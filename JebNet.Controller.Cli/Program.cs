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
        private const int WAIT_PERIOD = 500;

        private static ControllerService controllerService = new ControllerService();
        private static bool cancelPressed = false;

        static void Main(string[] args)
        {
            Console.WriteLine("Press enter to launch vehicle.");
            Console.ReadLine();

            Console.WriteLine("Launching...");

            Console.CancelKeyPress += Console_CancelKeyPress;

            PidController throttleController = new PidController()
            {
                Kp = 0.005,
                Ki = 0.0,
                Kd = 0.5,
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

            var startTime = DateTime.Now;
            while (!cancelPressed)
            {
                var deltaTime = DateTime.Now - startTime;

                var deltaThrottle = throttleController.control(2000, vessel.Altitude, (int)deltaTime.TotalMilliseconds);
                throttle += deltaThrottle;
                throttle = Math.Max(0.0, throttle);
                throttle = Math.Min(1.0, throttle);

                vessel.ControlState.Pitch = 0;
                vessel.ControlState.Yaw = 0;

                var loopControlState = new ControlState(vessel.ControlState);
                loopControlState.MainThrottle = (float)throttle;

                Console.WriteLine("Throttle: {0:F2}%. Delta throttle: {1:F2} Altitude: {2:F2}", throttle * 100, deltaThrottle * 100, vessel.Altitude);

                vessel = controllerService.SendVessel(loopControlState);

                Thread.Sleep(WAIT_PERIOD);
            }

        }

        private static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            cancelPressed = true;
        }
    }
}
