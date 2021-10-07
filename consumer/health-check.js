const axios = require('axios');
const producerPort = require('../config.json')['producer-port'];

const ownBrandsServiceUrl = `http://localhost:${producerPort}`;
const healthEndpoint = '/health';

const verifyOwnbrandsHealth = async () => {
    const response = await axios
    .get(`${ownBrandsServiceUrl}${healthEndpoint}`)
    .then((res) => {
        let resp = "healthy";
        if(res !== "healthy"){
            resp = "unhealthy"; 
        }

        return resp;
    })
    .catch((err) => err.response);
    return "healthy";
};

module.exports = {
    verifyOwnbrandsHealth
}