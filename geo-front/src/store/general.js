/* eslint-disable */
import { SET_ERROR, PURGE_ERROR, SET_PROCESSING, UPLOAD_IMG, SHOW_PRELOADER, HIDE_PRELOADER, CLEAR_ALL, UPDATE_NOTIFY } from './mutationsType';
import { FATAL_ERROR, BAD_REQUEST } from './actionsType';

const state = {
  processing: false,
  preloader: false,
  uploadImg: false,
  error: null,
  notify: {
    color: null,
    message: null,
    show: true,
  }
};

const actions = {
  [FATAL_ERROR]({ commit }, msg) {
    
  },

  [BAD_REQUEST]({ commit }, payload) {    
    commit(SET_ERROR, payload);  
  },
};

const mutations = {
  [SHOW_PRELOADER](state) {
    state.preloader = true;
  },
  [HIDE_PRELOADER](state) {
    state.preloader = false;
  },
  [SET_ERROR](state, payload) {
    state.error = payload;
  },
  [PURGE_ERROR](state) {
    state.error = null;
  },
  [SET_PROCESSING](state, payload) {
    if(payload){
      state.error = null;
    }
    state.processing = payload;
  },
  [CLEAR_ALL](state){
    state.error = null;
    state.processing = false;
  },
  [UPLOAD_IMG](state, payload) {
    if(payload){
      state.error = null;
    }
    state.uploadImg = payload;
  },
  [UPDATE_NOTIFY](state, payload) {
    state.notify.color = payload.isError ? 'error' : 'success';
    state.notify.message = payload.msg;
    state.notify.show = !state.notify.show;
  },
};

const getters = {
  getProcessing: (state) => state.processing,
  isUpload: (state) => state.uploadImg,
  getError: (state) => state.error,
  showPreloader: (state) => state.preloader,
  nMsg: (state) => state.notify.message,
  nColor: (state) => state.notify.color,
  nShow: (state) => state.notify.show,
};

export default {
  state,
  actions,
  mutations,
  getters,
};
