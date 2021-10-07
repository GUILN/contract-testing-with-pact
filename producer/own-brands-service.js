const express = require('express');
const port = process.env.PORT || 3004;

const app = express();


app.get('/health', (req, res) => {
    console.log('verifyin own brands service health...');
    res.send("healthy");
});

app.listen(port, () => console.log(`Listening on port ${port}...`));
