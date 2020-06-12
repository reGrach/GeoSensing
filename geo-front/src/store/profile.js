/* eslint-disable */
import ProfileApi from '../api/profile';
import { GET_PROFILE, UPDATE_PROFILE } from './actionsType';
import { SET_ERROR, SET_PROCESSING, PURGE_ERROR, SET_PROFILE } from './mutationsType';

const state = {
  formProfile: {
    name: null,
    surName: null,
    avatar: null
  },
};

const actions = {
  [GET_PROFILE]({ commit }) {
    commit(SET_PROCESSING, true);
    commit(PURGE_ERROR);
    return new Promise((resolve, reject) => {
      ProfileApi.Get()
        .then(({ data }) => {
          commit(SET_PROFILE, data);
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
  [UPDATE_PROFILE]({ commit }, profileData) {
    commit(SET_PROCESSING, true);
    commit(PURGE_ERROR);
    return new Promise((resolve, reject) => {
      ProfileApi.Update(profileData)
        .then(({ data }) => {
          commit(SET_PROFILE, data);
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
};

const mutations = {
  [SET_PROFILE](state, payload) {
    state.formProfile.name = payload.name;
    state.formProfile.surName = payload.surName;
  },
  [PURGE_AUTH](state) {
  },
  [INIT_AUTH](state) {
  },
};

const getters = {
};

export default {
  state,
  actions,
  mutations,
  getters,
};
