using System;
using Microsoft.Extensions.Logging;

namespace CrmWrapperApi.Connector
{
    public class ProviderConnector
    {
        private readonly string _ownbrandsEndpoint;
        private readonly ILogger _logger;
        public ProviderConnector(string ownbrandsEndpoint, ILogger logger)
        {
            _ownbrandsEndpoint = ownbrandsEndpoint;
            _logger = logger;
        }
        public string CheckProvidersHealth() 
        {
            _logger.LogInformation($"Calling Own brands endpoint: {_ownbrandsEndpoint}");
            return "Healthy";
        }
    }

}