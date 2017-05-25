using JebNet.Controller.Domain.Integration;
using JebNet.Controller.Domain.Mapper;
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

        private VesselMapper vesselMapper = new VesselMapper();

        public Vessel FetchVesselStatus()
        {
            string endpoint = Constants.SERVER_URL + Constants.STATUS_ENDPOINT;
            var integrationResponseVessel = integrationService.Retrieve<JebNet.Controller.Integration.Domain.Vessel>(endpoint);
            var responseVessel = vesselMapper.MapToDomain(integrationResponseVessel);
            return responseVessel;
        }

        public Vessel SendVessel(Vessel vessel)
        {
            string endpoint = Constants.SERVER_URL + Constants.STATUS_ENDPOINT;
            var integrationVessel = vesselMapper.MapToIntegration(vessel);
            var integrationResponseVessel = integrationService.Send<JebNet.Controller.Integration.Domain.Vessel>(endpoint, integrationVessel);
            var responseVessel = vesselMapper.MapToDomain(integrationResponseVessel);
            return responseVessel;
        }
    }
}
