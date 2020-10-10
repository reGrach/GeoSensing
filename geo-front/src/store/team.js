/* eslint-disable */
import TeamApi from '../api/team';
import { GET_ALL_TEAMS, CREATE_TEAM, JOIN_TEAM, CHECK_AUTH, GET_TEAM, LEAVE_TEAM } from './actionsType';
import { SET_ERROR, SET_PROCESSING, SHOW_PRELOADER, HIDE_PRELOADER, SET_TEAM, REMOVE_TEAM } from './mutationsType';

const state = {
  currentTeam: {},
  participants: []
};

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
          commit(SET_TEAM, data);
          resolve();
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
          dispatch(GET_TEAM);
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
    return new Promise((resolve, reject) => {
      TeamApi.join(TeamId)
        .then(() => {
          dispatch(CHECK_AUTH);
          dispatch(GET_TEAM);
          resolve();
        })
        .catch(({ response }) => {
          console.log(response);
          commit(SET_ERROR, response);
          reject(response);
        })
    });
  },

  [LEAVE_TEAM]({ commit, dispatch }, userLogin) {
    if(userLogin) {
      return new Promise((resolve, reject) => {
        TeamApi.exclude(userLogin)
          .then(() => { 
            dispatch(GET_TEAM);
            resolve(); 
          })
          .catch(({ response }) => {
            console.log(response);
            commit(SET_ERROR, response);
            reject(response);
          })
      });
    } else {
      return new Promise((resolve, reject) => {
        commit(SHOW_PRELOADER);
        TeamApi.leave()
          .then(() => {
            dispatch(CHECK_AUTH);
            commit(REMOVE_TEAM);
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

const mutations = {
  [SET_TEAM](state, payload) {
    if(payload){
      state.currentTeam = {
        color: payload.color, 
        title: payload.title, 
        createDate: payload.createDate 
      };
      state.participants = payload.participants;
    }
  },
  [REMOVE_TEAM](state) {
    state.currentTeam = {};
    state.participants = [];
  },
};

const getters = {
  getTeam: (state) => state.currentTeam,
  getParticipants: (state) => state.participants,
};

export default {
  state,
  actions,
  mutations,
  getters,
};
