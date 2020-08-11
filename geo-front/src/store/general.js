/* eslint-disable */
import { SET_ERROR, PURGE_ERROR, SET_PROCESSING, UPLOAD_IMG } from './mutationsType';

const state = {
  processing: false,
  uploadImg: false,
  error: null,
};

const mutations = {
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
  [UPLOAD_IMG](state, payload) {
    if(payload){
      state.error = null;
    }
    state.uploadImg = payload;
  },
};

const getters = {
  getProcessing: (state) => state.processing,
  isUpload: (state) => state.uploadImg,
  getError: (state) => state.error,
};

export default {
  state,
  mutations,
  getters,
};
