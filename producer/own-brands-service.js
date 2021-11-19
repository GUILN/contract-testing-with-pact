const express = require('express');
const producerPort = require('../config.json')['producer-port'];
const port = producerPort;

const app = express();

const healthResponse = {healthStatus: "healthy"};

app.get('/health', (req, res) => {
    console.log('verifyin own brands service health...');
    console.log(`response being sent: ${healthResponse}`);
    res.set('Content-Type', 'text/html; charset=UTF-8');
    res.send(healthResponse);
});

const server = app.listen(port, () => console.log(`Listening on port ${port}...`));
//server.close();
