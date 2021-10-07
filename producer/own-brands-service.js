const express = require('express');
const producerPort = require('../config.json')['producer-port'];
const port = process.env.PORT || producerPort;

const app = express();


app.get('/health', (req, res) => {
    console.log('verifyin own brands service health...');
    res.send("healthy");
});

app.listen(port, () => console.log(`Listening on port ${port}...`));
