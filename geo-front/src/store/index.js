import Vue from 'vue';
import Vuex from 'vuex';
import auth from './auth';
import general from './general';

Vue.use(Vuex);

const store = new Vuex.Store({
  modules: {
    auth, general,
  },
});

export default store;
