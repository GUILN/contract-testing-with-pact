using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace CrmWrapperApi.Connector
{
    public class ProviderConnector
    {
        private readonly string _ownbrandsEndpoint;
        private readonly ILogger _logger;

        private readonly HttpClient _httpClient = new HttpClient(); 
        public ProviderConnector(string ownbrandsEndpoint, ILogger logger)
        {
            _ownbrandsEndpoint = ownbrandsEndpoint;
            _logger = logger;
        }
        public Task<string> CheckProvidersHealth() 
        {
            _logger.LogInformation($"Calling Own brands endpoint: {_ownbrandsEndpoint}");
            
            return GetOwnBrandsHealth();
        }

        private async Task<string> GetOwnBrandsHealth()
        {
            var ownBrandsHealthStatus = string.Empty;
            try
            {
                ownBrandsHealthStatus = await _httpClient.GetStringAsync(_ownbrandsEndpoint);
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong during the http call");
            }
            
            return ownBrandsHealthStatus;
        }
    }

}