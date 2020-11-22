/* eslint-disable */
import AuthApi from '../api/auth';
import router from '../router'
import roles from '../common/roles'
import { SIGN_IN, SIGN_UP, SIGN_OUT, CHECK_AUTH } from './actionsType';
import
{
  SET_AUTH,
  PURGE_AUTH,
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
    AuthApi.signin(credentials)
    .then(({ data }) => {
      commit(SET_AUTH, data);
      router.push('/')
    })
    .catch(() => {})
    .finally(() => {
      commit(SET_PROCESSING, false);
    });

  },

  [SIGN_UP]({ commit }, credentials) {
    commit(SET_PROCESSING, true);
    AuthApi.signup(credentials)
    .then(({ data }) => {
      commit(SET_AUTH, data);
      router.push('/profile');
    })
    .catch(() => {})
    .finally(() => {
      commit(SET_PROCESSING, false);
    });
  },

  [SIGN_OUT]({ commit }) {
    commit(PURGE_AUTH);
    AuthApi.signout()
    .then(() => router.push('/signin'))
    .catch(() => {});
  },

  [CHECK_AUTH]({ commit }) {
    return new Promise((resolve) => {
      AuthApi.check()
      .then(({ data }) => {
        commit(SET_AUTH, data);
      })
      .catch(() => {
        commit(PURGE_AUTH);
      })
      .finally(() => resolve())
    });
  },
};

const mutations = {
  [SET_AUTH](state, { login, role, avatarSrc }) {
    state.isAuthenticated = true;
    state.userLogin = login;
    state.userRole = role;
    state.userAvatar = avatarSrc;
  },
  [SET_AVA](state, avatarSrc){
    state.userAvatar = avatarSrc;
  },
  [PURGE_AUTH](state) {
    state.isAuthenticated = false;
    state.userRole = null;
    state.userLogin = null;
    state.userAvatar = null;
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
