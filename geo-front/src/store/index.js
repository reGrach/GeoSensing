import Vue from 'vue';
import Vuex from 'vuex';
import auth from './auth';
import general from './general';
import profile from './profile';

Vue.use(Vuex);

const store = new Vuex.Store({
  modules: {
    auth, general, profile
  },
});

export default store;
