/* eslint-disable */
import TeamApi from '../api/team';
import { GET_ALL_TEAMS, CREATE_TEAM, JOIN_TEAM, CHECK_AUTH, GET_TEAM, LEAVE_TEAM } from './actionsType';
import { SET_ERROR, SET_PROCESSING, SHOW_PRELOADER, HIDE_PRELOADER } from './mutationsType';

const state = { };

const actions = {
  [GET_ALL_TEAMS]({ commit, getters }) {
    commit(SHOW_PRELOADER);
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
          commit(HIDE_PRELOADER);
        });
    });
  },
  [GET_TEAM]({ commit }, id) {
    commit(SHOW_PRELOADER);
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
          commit(HIDE_PRELOADER);
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
  [LEAVE_TEAM]({ commit, dispatch }, userLogin) {
    commit(SET_PROCESSING, true);    
    if(userLogin) {
      return new Promise((resolve, reject) => {
        TeamApi.exclude(userLogin)
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
    } else {
      return new Promise((resolve, reject) => {
        TeamApi.leave()
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
    }    
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
