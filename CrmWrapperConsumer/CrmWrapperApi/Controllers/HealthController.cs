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
        private readonly OwnBrandsConnector _ownBrandsConnector;
        private readonly List<Func<Task<string>>> _dependenciesHealthCheckFunctions;
        public HealthController(ILogger<HealthController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _ownBrandsConnector = new OwnBrandsConnector(_configuration["CobrandsEndpoint"], _logger);
            _dependenciesHealthCheckFunctions = new List<Func<Task<string>>>
            {
                () =>  _ownBrandsConnector.CheckProvidersHealth(),
                () => Task<string>.Run(() => "Healthy")
            };
        }
        [HttpGet]
        public async Task<string> Get()
        {
            var health = new Health { HealthStatus = "healthy"};

            foreach (var function in _dependenciesHealthCheckFunctions)
            {
                var result = await function();
                if (result.ToLower() != "Healthy".ToLower())
                {
                    health.HealthStatus = "unhealthy";
                    break;
                }
            }
            
            return health.HealthStatus;
        }

    }
}