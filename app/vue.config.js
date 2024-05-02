const { defineConfig } = require("@vue/cli-service");
module.exports = defineConfig({
  transpileDependencies: true,
  lintOnSave: false,
  //代理跨域
  devServer: {
    proxy: {
      "/api": {
        target: "https://localhost:44356/",
        changeOrigin: true,
        secure: false, // 如果是自签名证书，需要设置为 false
        pathRewrite: { '^/api': '' }
      }
    },
  },
});
