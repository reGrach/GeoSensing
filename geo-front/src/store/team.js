/* eslint-disable */
import TeamApi from '../api/team';
import { GET_ALL_TEAMS, CREATE_TEAM, JOIN_TEAM, CHECK_AUTH, GET_TEAM, LEAVE_TEAM } from './actionsType';
import { SET_PROCESSING, SHOW_PRELOADER, HIDE_PRELOADER, SET_TEAM, REMOVE_TEAM } from './mutationsType';

const state = {
  currentTeam: {},
  participants: []
};

const actions = {
  [GET_ALL_TEAMS]({ commit, getters }) {
    commit(SHOW_PRELOADER);
    return new Promise((resolve) => {
      TeamApi.getAll(getters.isAdmin)
        .then(({ data }) => {
          resolve(data);
        })
        .finally(() => {
          commit(HIDE_PRELOADER);
        });
    });
  },
  [GET_TEAM]({ commit }, id) {
    commit(SHOW_PRELOADER);
    return new Promise((resolve) => {
      TeamApi.get(id)
        .then(({ data }) => {
          commit(SET_TEAM, data);
          resolve();
        })
        .finally(() => {
          commit(HIDE_PRELOADER);
        });
    });
  },
  [CREATE_TEAM]({ commit, dispatch }, team) {
    commit(SET_PROCESSING, true);
    return new Promise((resolve) => {
      TeamApi.create(team)
        .then(() => {
          dispatch(CHECK_AUTH);
          dispatch(GET_TEAM);
          resolve();
        })
        .finally(() => {
          commit(SET_PROCESSING, false);
        });
    });
  },
  [JOIN_TEAM]({ dispatch }, TeamId) {
    return new Promise((resolve) => {
      TeamApi.join(TeamId)
        .then(() => {
          dispatch(CHECK_AUTH);
          dispatch(GET_TEAM);
          resolve();
        })
    });
  },

  [LEAVE_TEAM]({ commit, dispatch }, userLogin) {
    if(userLogin) {
      return new Promise((resolve) => {
        TeamApi.exclude(userLogin)
          .then(() => { 
            dispatch(GET_TEAM);
            resolve(); 
          })
      });
    } else {
      return new Promise((resolve) => {
        commit(SHOW_PRELOADER);
        TeamApi.leave()
          .then(() => {
            dispatch(CHECK_AUTH);
            commit(REMOVE_TEAM);
            resolve();
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
