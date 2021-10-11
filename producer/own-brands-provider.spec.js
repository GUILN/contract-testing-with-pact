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
    test('Should validate the expectations of our consumer', () => {
        return new Verifier(verifierOptions).verifyProvider();
    });
});

