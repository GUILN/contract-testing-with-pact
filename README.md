# This is repo to exemplify how contract testing could benefit us

## In order to run the example in this repo you will need to have installed:
    - `Dotnet 5.0 SDK`
    - `Node and Npm` (if you prefer you can use yarn)
    - `Docker`

## Main goal of this Example

The main goal of this repo is to exemplify how contract testing (using PACTIO) could benefit, and prevent us from breaking each others functionalities by ensuring a contract is respected between both parts (consumer and provider). 
In to compose this example we will have 3 components interacting between themselves:

-  `OwnBrands Provider`, which is a service consumed by both `CrmWrapper`service and `Another Consumer`. This service will only expose the `HealthCheck` endpoint
- `CrmWrapper` which in this example will only expose the `Hea thCheck` Endpoint and will only consume the `OwnBrands Provider` `HalthCheck` endpoint
- `Another` which will only expose the `HealthCheck` Endpoint and will only consume the `OwnBrands Provider` `HalthCheck` endpoint

All the services will only interact between themselves to make the `HealthCheck` endpoint work.

## Component Diagram to explain the interaction between the services:

# Steps to follow in order to simulate the workflow (I highly recomend you run this in a terminal multiplexer such as `Tmux` them you will be able to see the whole interaction)

### Running consumer's contract test and Generating Consumer's PACT contract
- 1 in root folder run `npm run test:consumer` - This will generate the pact contract for `Node` consumer
- 2 `cd` into dotnet's service folder and run `dotnet test` this will generate pact contract for the dotnet `consumer`

### Starting up PACT broker docker container and publishing PACTs that was generated in steps before

### Running provider's contract tests

### Changing provider's response and seeing the provider's contract testing failing (breaking the contract) 

### Conclusion

 - At this point you can go within two 

## Sequence Diagram to explain how the component flow of component testing should work
