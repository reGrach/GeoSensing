/* eslint-disable */
import GeoApi from '../api/geo';
import { NEW_EXP, GET_LIST_EXP, POINT, GET_LIST_POINTS } from './actionsType';
import { SET_ERROR, SET_PROCESSING, SET_EXP, FILL_EXP } from './mutationsType';
const state = {
  experiments: [],
};
const actions = {
  [GET_LIST_EXP]({ commit }) {
    commit(SET_PROCESSING, true);
    return new Promise((resolve, reject) => {
      GeoApi.GetMy()
        .then(({ data }) => {
          const payload = {
            list: data,
            add: false
          };
          commit(FILL_EXP, payload)
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

  [NEW_EXP]({ commit }, title) {
    commit(SET_PROCESSING, true);
    return new Promise((resolve, reject) => {
      GeoApi.InitExperiment(title)
        .then(({ data }) => {
          const payload = {
            add: true,
            list: data,
          }
          commit(FILL_EXP, payload)
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

  [POINT]({ commit }, point) {
    commit(SET_PROCESSING, true);
 
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
  [GET_LIST_POINTS]({ commit }) {
    commit(SET_PROCESSING, true);
    return new Promise((resolve, reject) => {
      GeoApi.GetMyPoints()
        .then(({ data }) => {
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

};

const mutations = {
  [FILL_EXP](state, payload) {
    if (payload.add) {
      state.experiments.push(payload.list)
    } else {
      state.experiments = payload.list;
    }
  }
};

const getters = {
  getListExperiments: (state) => state.experiments,
};

export default {
  state,
  actions,
  mutations,
  getters,
};
