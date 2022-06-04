const PROXY_CONFIG = [
  {
    context: [
      "/api",
    ],
    target: "https://localhost:7117",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
