//对于axios进行二次封装
import axios from "axios";
import nprogress from "nprogress";
import "nprogress/nprogress.css";

const requests = axios.create({
  //基础路径,发请求URL携带api【发现:真实服务器接口都携带/api】
  baseURL: "https://localhost:44356/",
  //超时的设置
  timeout: 5000,
});

requests.interceptors.request.use((config) => {
  nprogress.start();
  return config;
});

requests.interceptors.response.use(
  (res) => {
    nprogress.done();
    return res.data;
  },
  (error) => {
    nprogress.done();
    return Promise.reject(error);
  }
);

//最后需要暴露:暴露的是添加新的功能的axios,即为requests
export default requests;
