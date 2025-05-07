// File: frontend/src/setupProxy.js
const { createProxyMiddleware } = require('http-proxy-middleware');

module.exports = function(app) {
  // يوجّه كل طلب يبدأ بـ /api إلى http://localhost:5160
  app.use(
    '/api',
    createProxyMiddleware({
      target: 'http://localhost:5160',
      changeOrigin: true,
    })
  );
};
