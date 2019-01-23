import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store";
import ElementUI from "element-ui";
import "element-ui/lib/theme-chalk/index.css";

import iView from "iview";
import "iview/dist/styles/iview.css";
// 引用API文件
import api from "./api/http.js";
// 将API方法绑定到全局
Vue.prototype.$api = api;

Vue.use(iView);
Vue.use(ElementUI);

Vue.config.productionTip = false;

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount("#app");
