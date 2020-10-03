/* eslint-disable */
import GeoApi from '../api/geo';
import { NEW_EXP, GET_LIST_EXP, POINT } from './actionsType';
import { SET_ERROR, SET_PROCESSING, SET_EXP } from './mutationsType';

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

    [POINT]({ commit }, coords, mode, cExpId) {
      commit(SET_PROCESSING, true);
      const point = {
        ...coords,
        experimentId: cExpId,
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

};

const getters = {};

export default {
  actions,
  mutations,
  getters,
};
