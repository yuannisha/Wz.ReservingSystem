import Vue from "vue";
import VueRouter from "vue-router";
import routes from "./routes";

Vue.use(VueRouter);

//先把VueRouter原型对象的push和replace先保存一份
let originPush = VueRouter.prototype.push;
let originReplace = VueRouter.prototype.replace;

//重写push、replace
//参数1：往哪里跳转 参数2：成功回调 参数3：失败回调
VueRouter.prototype.push = function (location, resolve, reject) {
  if (resolve && reject) {
    originPush.call(this, location, resolve, reject);
  } else {
    originPush.call(
      this,
      location,
      () => { },
      () => { }
    );
  }
};
VueRouter.prototype.replace = function (location, resolve, reject) {
  if (resolve && reject) {
    originReplace.call(this, location, resolve, reject);
  } else {
    originReplace.call(
      this,
      location,
      () => { },
      () => { }
    );
  }
};

let router = new VueRouter({
  routes,
  // 使得每一次路由跳转后，滑轮的位置总在最上边
  scrollBehavior(to, from, savedPosition) {
    return { y: 0 };
  },
});

router.beforeEach((to, from, next) => {
  next();
});

export default router;
