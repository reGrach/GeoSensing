/* eslint-disable */
import GeoApi from '../api/geo';
import { NEW_EXP, GET_LIST_EXP, POINT } from './actionsType';
import { SET_ERROR, SET_PROCESSING, SET_EXP } from './mutationsType';

const state = {
  currentExperiment: null,
};

const actions = {
    [GET_LIST_EXP]({ commit }) {
        commit(SET_PROCESSING, true);
        return new Promise((resolve, reject) => {
            GeoApi.GetMy()
            .then(({data}) => {
              resolve(data);
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

    [NEW_EXP]({ commit }, title) {
      commit(SET_PROCESSING, true);
      return new Promise((resolve, reject) => {
          GeoApi.InitExperiment(title)
          .then(({data}) => {
            resolve(data);
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

    [POINT]({ commit, state }, coords, mode) {
      commit(SET_PROCESSING, true);
      const point = {
        ...coords,
        experimentId: state.currentExperiment,
        mode: mode
      }
      return new Promise((resolve, reject) => {
          GeoApi.FixCoords(point)
          .then(() => resolve())
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

const mutations = {
  [SET_EXP](state, payload) {
    state.currentExperiment = payload;
  },
};

const getters = {};

export default {
  state,
  actions,
  mutations,
  getters,
};
