const config = require('../config.json');
const { Verifier } = require('@pact-foundation/pact');

const verifierOptions = {
    provider: 'Provider',
    providerBaseUrl: `http://localhost:${config['producer-port']}`,
    pactBrokerUrl: process.env.PACT_BROKER_URL,
    publishVerificationResult: true,
    providerVersion: '1.0.0',
    logLevel: 'INFO'
};

describe('Pact verification', () => {
    // Arrange Act and Assert steps are validated automatically since:
    // Arrange step is done by fetching the contracts in contract broker and creating the requests
    // regarding every contract configured to be fetched
    // Act step is done by firing each request against the real service (which endpoint is passed along within the providerBaseUrl pact options property)
    // Assert is also done automatically by the verifyProvider method which them calls the assertions specified in each of the pacts fetched in the beginning 
    // of the providers test 
    test('Should validate the expectations of our consumer', () => {
        return new Verifier(verifierOptions).verifyProvider();
    });
});

