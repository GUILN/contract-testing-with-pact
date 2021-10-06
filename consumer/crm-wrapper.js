const express = require('express');
const port = process.env.PORT || 3002;

const app = express();

app.get('/health', (req, res) => {
    res.send('healthy');
});

app.listen(port, () => console.log(`Listening on port ${port}...`));
