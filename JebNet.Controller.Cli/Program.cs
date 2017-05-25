using JebNet.Controller.Domain;
using JebNet.Controller.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JebNet.Controller.Cli
{
    class Program
    {
        private static ControllerService controllerService = new ControllerService();

        static void Main(string[] args)
        {
            Console.WriteLine("Press enter to launch vehicle.");
            Console.ReadLine();

            Vessel vessel = controllerService.FetchVesselStatus();
            vessel.StageVessel();
            
            controllerService.SendVessel(vessel);

            Console.WriteLine("Vehicle launched.");
            Console.ReadLine();
        }
    }
}
