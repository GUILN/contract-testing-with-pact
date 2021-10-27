using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace CrmWrapperApi.Connector
{
    public class OwnBrandsConnector
    {
        private readonly string _ownbrandsEndpoint;
        private readonly ILogger _logger;

        private readonly HttpClient _httpClient = new HttpClient(); 
        public OwnBrandsConnector(string ownbrandsEndpoint, ILogger logger)
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
            var httpStatusCode = HttpStatusCode.Unused;
            var reasonPhrase = String.Empty;
            try
            {
                var request = CreateRequest();
                var response = await _httpClient.SendAsync(request);
                ownBrandsHealthStatus = await response.Content.ReadAsStringAsync();

                httpStatusCode = response.StatusCode;
                reasonPhrase = response.ReasonPhrase;
                
                request.Dispose();
                response.Dispose();
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong during the http call");
            }

            if (httpStatusCode != HttpStatusCode.OK)
            {
                var httpException = new HttpRequestException($"Something went wrong during the request to OwnBrands HealthCheck | Reason Phrase: {reasonPhrase}",
                    null ,statusCode: httpStatusCode);
                _logger.LogError(httpException.Message);
                throw httpException;
            }
            
            return ownBrandsHealthStatus;
        }

        private HttpRequestMessage CreateRequest()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,_ownbrandsEndpoint);
            request.Headers.Add("Accept", "application/json");
            
            return request;
        }
    }

}