/* eslint-disable */
import TeamApi from '../api/team';
import { GET_ALL_TEAMS, CREATE_TEAM, JOIN_TEAM, CHECK_AUTH, GET_TEAM } from './actionsType';
import { SET_ERROR, SET_PROCESSING } from './mutationsType';

const state = {};

const actions = {
  [GET_ALL_TEAMS]({ commit, getters }) {
    commit(SET_PROCESSING, true);
    return new Promise((resolve, reject) => {
      TeamApi.getAll(getters.isAdmin)
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
  [GET_TEAM]({ commit }, id) {
    commit(SET_PROCESSING, true);
    return new Promise((resolve, reject) => {
      TeamApi.get(id)
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
  [CREATE_TEAM]({ commit, dispatch }, team) {
    commit(SET_PROCESSING, true);
    return new Promise((resolve, reject) => {
      TeamApi.create(team)
        .then(() => {
          dispatch(CHECK_AUTH);
          resolve();
        })
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
  [JOIN_TEAM]({ commit, dispatch }, TeamId) {
    commit(SET_PROCESSING, true);
    return new Promise((resolve, reject) => {
      TeamApi.join(TeamId)
        .then(() => {
          dispatch(CHECK_AUTH);
          resolve();
        })
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
};

const mutations = {};

const getters = {};

export default {
  state,
  actions,
  mutations,
  getters,
};
