using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using CrmWrapperApi.Connector;

namespace DefaultNamespace
{
    [ApiController]
    [Route("[Controller]")]
    public class HealthController : ControllerBase
    {
        private readonly ILogger<HealthController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ProviderConnector _providerConnector;
        private readonly List<Func<string>> _dependenciesHealthCheckFunctions;
        public HealthController(ILogger<HealthController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _providerConnector = new ProviderConnector(_configuration["CobrandsEndpoint"], _logger);
            _dependenciesHealthCheckFunctions = new List<Func<string>>
            {
                () => "Healthy",
                () => "Healthy",
                () => _providerConnector.CheckProvidersHealth()
            };
        }
        [HttpGet]
        public string Get()
        {
            var health = new Health { HealthStatus = "healthy"};
            
            if (_dependenciesHealthCheckFunctions.Any(f => f() != "Healthy"))
            {
                health.HealthStatus = "unhealthy";
            }

            return health.HealthStatus;
        }

    }
}