using JebNet.Controller.Domain.Integration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JebNet.Controller.Domain.Service
{
    public class ControllerService
    {
        private IntegrationService integrationService = new IntegrationService();

        public Vessel FetchVesselStatus()
        {
            string endpoint = Constants.SERVER_URL + Constants.STATUS_ENDPOINT;
            Vessel vessel = integrationService.Retrieve<Vessel>(endpoint);
            return vessel;
        }

        public Vessel SendVessel(Vessel vessel)
        {
            string endpoint = Constants.SERVER_URL + Constants.STATUS_ENDPOINT;
            Vessel responseVessel = integrationService.Send<Vessel>(endpoint, vessel);
            return responseVessel;
        }
    }
}
