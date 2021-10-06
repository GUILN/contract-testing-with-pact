const express = require('express');
const port = process.env.PORT || 3004;

const app = express();

const healthObject = {
    status: 1,
    isHealthy: true,
    serviceName: "own brands service"
};

app.get('/health', (req, res) => {
    res.send("healthy");
});

app.listen(port, () => console.log(`Listening on port ${port}...`));
