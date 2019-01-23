import Vue from "vue";
import Router from "vue-router";
import Home from "./views/Home.vue";
import Login from "./views/Login.vue";

Vue.use(Router);

const router = new Router({
  mode: "history",
  base: process.env.BASE_URL,
  routes: [
    {
      path: "/",
      name: "home",
      component: Home,
      meta: {
        requireAuth: true
      }
    },
    {
      path: "/login",
      name: "Login",
      component: Login
    }
  ]
});

router.beforeEach((to, from, next) => {
  if (to.meta.requireAuth) {
    // 判断该路由是否需要登录权限
    if (window.localStorage.Token && window.localStorage.Token.length >= 128) {
      //通过vuexstate获取当前的token是否存在
      next();
    } else {
      next({
        path: "/login",
        query: { redirect: to.fullPath } // 将跳转的路由path作为参数，登录成功后跳转到该路由
      });
    }
  } else {
    next();
  }
})

export default router;
