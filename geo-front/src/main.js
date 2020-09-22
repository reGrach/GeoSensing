import Vue from 'vue';
import App from './App.vue';
import router from './router';
import store from './store';
import { CHECK_AUTH } from './store/actionsType';
import vuetify from './plugins/vuetify';
import ApiService from './api';

ApiService.init();

const app = {
  router,
  store,
  vuetify,
  render: (h) => h(App),
};

store.dispatch(CHECK_AUTH)
  .finally(() => {
    new Vue(app).$mount('#app');
  });
