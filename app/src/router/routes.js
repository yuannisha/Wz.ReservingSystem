import Login from "@/pages/Login";
import Register from "@/pages/Register";
import Main from "@/pages/backend/Main.vue";
import BackHome from "@/pages/backend/Home/index.vue";
import BackUser from "@/pages/backend/User.vue";

export default [
  {
    path: "/login",
    component: Login,
    meta: { isShow: false, requireAuth: true },
  },
  {
    path: "/register",
    component: Register,
    meta: { isShow: false },
  },
  {
    path: "/back",
    component: Main,
    meta: { requireCheck: true },
    redirect: "/back/home",
    children: [
      {
        path: "/back/home",
        component: BackHome,
        meta: { requireCheck: true },
      },
      {
        path: "/back/user",
        component: BackUser,
        meta: { requireCheck: true },
      },
      {
        path: "/back/order",
        component: () => import('@/pages/backend/Order.vue'),
        meta: { requireCheck: true },
      },
      {
        path: "/back/reset",
        component: () => import("@/pages/backend/Reset.vue"),
        meta: { requireCheck: true },
      },
      {
        path: "/back/info",
        component: () => import("@/pages/backend/Info.vue"),
        meta: { requireCheck: true },
      },
      {
        path: '/back/order/detail',
        component: () => import('@/pages/backend/OrderDetail.vue'),
        meta: { requireCheck: true }
      }
    ],
  },

  //重定向，项目跑起来的时候立马访问首页
  {
    path: "*",
    redirect: "/login",
  },
];
