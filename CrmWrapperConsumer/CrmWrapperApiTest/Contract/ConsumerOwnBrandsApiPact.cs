using System;
using PactNet;
using PactNet.Mocks.MockHttpService;
using PactNet.Models;

namespace CrmWrapperApiTest.Contract
{
    public class ConsumerOwnBrandsApiPact : IDisposable
    {
        public IPactBuilder PactBuilder { get; private set; }
        public IMockProviderService MockProviderService { get; private set; }

        public int MockServerPort => 9088;
        public string MockProviderServiceBaseUri => $"http://localhost:{MockServerPort}";

        public ConsumerOwnBrandsApiPact()
        {
            var consumerName = "CrmWrapper";
            var providerName = "OwnBrandsApi";
            PactBuilder = new PactBuilder(GetPactConfiguration);
            
            PactBuilder
                .ServiceConsumer(consumerName)
                .HasPactWith(providerName);

            MockProviderService = PactBuilder.MockService(MockServerPort);
        }

        private PactConfig GetPactConfiguration => new()
        {
            PactDir = @"../Pacts",
            LogDir = @"../Logs"
        };
        
        public void Dispose()
        {
            PactBuilder.Build(); // Note: will save pact file once finished
        }
    }
}