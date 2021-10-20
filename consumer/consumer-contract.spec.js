const { Pact } = require('@pact-foundation/pact');
const { like, eachLike } = require('@pact-foundation/pact').Matchers;
const { verifyOwnbrandsHealth } = require('./health-check');
const producerPort = require('../config.json')['producer-port'];

path = require('path');

const PORT = producerPort;

const provider = new Pact({
    consumer: 'Consumer',
    provider: 'Provider',
    port: PORT,
    log: path.resolve(process.cwd(), 'logs', 'pact.log'),
    dir: path.resolve(process.cwd(), 'pacts'),
    logLevel: 'INFO',
  });

describe('Own Brands Service', () => {
    describe('When a request to health check endpoint is made', () => {
      beforeAll(() =>
        // Arrange
        provider.setup().then(() => {

          provider.addInteraction({
            uponReceiving: 'a request to health check endpoint',
            withRequest: {
                method: 'GET',
                path: '/health'
            },  
            willRespondWith: {
              status: 200,
              body: eachLike(
                'healthy'
              ),  
            },  
          }); 
        })  
      );  
        
      // Act
      test('should contain expected data format which is a string storing either: healthy or unhealthy values', async () => {
        const response = await verifyOwnbrandsHealth();
        expect(response).toBe('healthy');
      }); 
  
  
      // Assert
      afterEach(() => provider.verify());
      afterAll(() => provider.finalize());
    }); 
  });
