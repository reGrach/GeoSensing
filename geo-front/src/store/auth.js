import AuthApi from '../api/auth';
import { SIGN_IN, SIGN_UP, SIGN_OUT } from './actionsType';
import {
  CHECK_AUTH, SET_AUTH, PURGE_AUTH, SET_ERROR, SET_PROCESSING, PURGE_ERROR,
} from './mutationsType';
import { getLogin, saveLogin, removeLogin } from '../common/localStorage';

const state = {
  userLogin: null,
  isAuthenticated: false,
};

const actions = {
  [SIGN_IN]({ commit }, credentials) {
    commit(SET_PROCESSING, true);
    commit(PURGE_ERROR, true);
    return new Promise((resolve, reject) => {
      AuthApi.signin(credentials)
        .then(({ data }) => {
          commit(SET_AUTH, data);
          resolve(data);
        })
        .catch(({ response }) => {
          console.log(response);
          commit(SET_ERROR, response.data);
          reject(response);
        })
        .finally(() => {
          commit(SET_PROCESSING, false);
        });
    });
  },
  [SIGN_UP]({ commit }, credentials) {
    commit(SET_PROCESSING, true);
    commit(PURGE_ERROR, true);
    return new Promise((resolve, reject) => {
      AuthApi.signup(credentials)
        .then(({ data }) => {
          resolve(data);
        })
        .catch(({ response }) => {
          console.log(response);
          commit(SET_ERROR, response.data);
          reject(response);
        })
        .finally(() => {
          commit(SET_PROCESSING, false);
        });
    });
  },
  [SIGN_OUT]({ commit }) {
    return new Promise((resolve, reject) => {
      AuthApi.signout()
        .then(({ data }) => {
          commit(PURGE_AUTH);
          resolve(data);
        })
        .catch(({ response }) => {
          console.log(response);
          reject(response);
        })
        .finally(() => {
          commit(SET_PROCESSING, false);
        });
    });
  },
};

const mutations = {
  [SET_AUTH]({ login, expiration }) {
    saveLogin(login, expiration);
    state.isAuthenticated = true;
    state.userLogin = login;
  },
  [PURGE_AUTH]() {
    removeLogin();
    state.isAuthenticated = false;
    state.userLogin = null;
  },
  [CHECK_AUTH]() {
    const login = getLogin();
    if (login) {
      state.isAuthenticated = true;
      state.userLogin = login;
    }
  },
};

const getters = {
  currentLogin: () => state.userLogin,
  isAuthenticated: () => state.isAuthenticated,
};

export default {
  state,
  actions,
  mutations,
  getters,
};
