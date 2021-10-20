using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CrmWrapperApi.Connector;
using Microsoft.Extensions.Logging;
using Moq;
using PactNet.Mocks.MockHttpService;
using PactNet.Mocks.MockHttpService.Models;
using Xunit;

namespace CrmWrapperApiTest.Contract.OwnBrandsApi
{
    public class HealthCheckContractTests : IClassFixture<ConsumerOwnBrandsApiPact>
    {
        private IMockProviderService _mockProviderService;
        private string _mockProviderServiceBaseUri;

        public HealthCheckContractTests(ConsumerOwnBrandsApiPact data)
        {
            _mockProviderService = data.MockProviderService;
            _mockProviderService.ClearInteractions(); // Note: clears any previously registered interactions before the test is run
            _mockProviderServiceBaseUri = data.MockProviderServiceBaseUri;
        }

        [Fact]
        public async Task 
            GivenARequestToGetHealthEndpointIsMade_WhenOwnBrandsApiIsHealthy_ThenWeReceiveExpectedResponseWithStatusCode200()
        {
            var expectedResponse = "healthy";
            // Arrange 
            _mockProviderService
                .Given("A GET request to healthCheck endpoint is made")
                .UponReceiving("A plain GET request")
                .With(new ProviderServiceRequest
                {
                    Method = HttpVerb.Get,
                    Path = "/health",
                    Headers = new Dictionary<string, object>
                    {
                        {"Accept", "application/json"}
                    }
                })
                .WillRespondWith(new ProviderServiceResponse
                {
                    Status = 200,
                    Body = expectedResponse,
                    Headers = new Dictionary<string, object>
                    {
                        {"Content-Type", "text/html; charset=UTF-8"}
                    }
                });

            var loggerMock = new Mock<ILogger>();
            var consumer = new OwnBrandsConnector(_mockProviderServiceBaseUri + "/health", loggerMock.Object);
            // Act 
            var result = await consumer.CheckProvidersHealth();

            // Assert
            Assert.Equal(expectedResponse, result);

            _mockProviderService.VerifyInteractions();
        }
    }
}