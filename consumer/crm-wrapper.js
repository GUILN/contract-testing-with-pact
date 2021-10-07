const { verifyOwnbrandsHealth } = require('./health-check');
const express = require('express');
const port = process.env.PORT || 3002;

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
