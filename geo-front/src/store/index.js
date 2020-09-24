import Vue from 'vue';
import Vuex from 'vuex';
import auth from './auth';
import general from './general';
import profile from './profile';
import team from './team';
import geo from './geo';

Vue.use(Vuex);

const store = new Vuex.Store({
  modules: {
    auth,
    general,
    profile,
    team,
    geo,
  },
});

export default store;
