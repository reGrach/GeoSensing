/* eslint-disable */
import AuthApi from '../api/auth';
import roles from '../common/roles'
import { SIGN_IN, SIGN_UP, SIGN_OUT, CHECK_AUTH } from './actionsType';
import { setAuth, purgeAuth, getAuth } from '../common/localStorage';
import
{
  INIT_AUTH,
  SET_AUTH,
  PURGE_AUTH,
  SET_ERROR,
  SET_PROCESSING,
  SET_AVA,
} from './mutationsType';

const state = {
  userLogin: null,
  userRole: null,
  userAvatar: null,
  isAuthenticated: false,
};

const actions = {
  [SIGN_IN]({ commit }, credentials) {
    commit(SET_PROCESSING, true);
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
    return new Promise((resolve, reject) => {
      AuthApi.signup(credentials)
        .then(() => { resolve(); })
        .catch(({ response }) => {
          console.log(response);
          commit(SET_ERROR, response);
          reject(response);
        })
        .finally(() => {
          commit(SET_PROCESSING, false);
        });
    });
  },
  [SIGN_OUT]({ commit }) {
    commit(PURGE_AUTH);
    return new Promise((resolve, reject) => {
      AuthApi.signout()
        .then(() => {
          resolve();
        })
        .catch(({ response }) => {
          console.log(response);
          reject(response);
        })
    });
  },
  [CHECK_AUTH]({ commit }) {
      AuthApi.check()
        .then(({ data }) => {
          commit(SET_AUTH, data);
        })
        .catch(({ response }) => {
          if(response.status === 401 && response.config){
            commit(PURGE_AUTH);            
          }
        })
  },
};

const mutations = {
  [SET_AUTH](state, { login, role, avatarSrc, expiration }) {
    setAuth(login, role, expiration);
    state.isAuthenticated = true;
    state.userLogin = login;
    state.userRole = role;
    state.userAvatar = avatarSrc;
  },
  [SET_AVA](state, avatarSrc){
    state.userAvatar = avatarSrc;
  },
  [PURGE_AUTH](state) {
    purgeAuth();
    state.isAuthenticated = false;
    state.userRole = null;
    state.userLogin = null;
    state.userAvatar = null;
  },
  [INIT_AUTH](state) {
    const {isAuth, login, role} = getAuth();
    if (isAuth) {
      state.isAuthenticated = true;
      state.userRole = role;
      state.userLogin = login;
    }
  },
};

const getters = {
  currentLogin: (state) => state.userLogin,
  getRoleName: (state) => roles[state.userRole],
  getAvatar: (state) => state.userAvatar,
  isAuthenticated: (state) => state.isAuthenticated,
  isAdmin: (state) => state.userRole === "Admin",
  isLeader: (state) => state.userRole === "Leader",
  isParticipant: (state) => state.userRole === "Participant",
  isEasyUser: (state) => state.userRole === "NonDefined",
};

export default {
  state,
  actions,
  mutations,
  getters,
};
