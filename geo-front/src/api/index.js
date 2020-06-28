import Vue from 'vue';
import axios from 'axios';
import VueAxios from 'vue-axios';

const ApiService = {
  init() {
    Vue.use(VueAxios, axios);
    // Vue.axios.defaults.baseURL = 'https://regrach.ru/api/';
    Vue.axios.defaults.baseURL = 'http://127.0.0.1:5000/api';
    Vue.axios.defaults.withCredentials = true;
  },

  query(route, params) {
    return Vue.axios.get(route, params);
  },

  get(route) {
    return Vue.axios.get(route);
  },

  post(route, params) {
    return Vue.axios.post(route, params);
  },
};

export default ApiService;
