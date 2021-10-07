const { verifyOwnbrandsHealth } = require('./health-check');
const express = require('express');
const consumerPort = require('../config.json')['consumer-port'];
const port = process.env.PORT || consumerPort;

const app = express();

app.get('/health', async (req, res) => {
    console.log('verifyin crm wrapper health...');
    if(await verifyOwnbrandsHealth() == "healthy"){
        res.send('healthy');
        return;    
    }
    res.send('unhealthy');
});

app.listen(port, () => console.log(`Listening on port ${port}...`));
