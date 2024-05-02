import Vue from "vue";
import App from "./App.vue";
//注册三级联动组件--全局组件
import router from "@/router";
//阻止生产报错
Vue.config.productionTip = false;

import ElementUI,{Message} from "element-ui";
import "element-ui/lib/theme-chalk/index.css";
Vue.use(ElementUI);

new Vue({
  render: (h) => h(App),
  beforeCreate() {
    Vue.prototype.$bus = this;//注册全局事件总线
    Vue.prototype.$message = Message;//消息提示框
  },
  router,
}).$mount("#app");
