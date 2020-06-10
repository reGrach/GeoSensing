import { SET_ERROR, PURGE_ERROR, SET_PROCESSING } from './mutationsType';

const state = {
  processing: false,
  error: null,
};

const mutations = {
  [SET_ERROR](payload) {
    state.error = payload;
  },
  [PURGE_ERROR]() {
    state.error = null;
  },
  [SET_PROCESSING](payload) {
    state.processing = payload;
  },
};

const getters = {
  getProcessing: () => state.processing,
  getError: () => state.error,
};

export default {
  state,
  mutations,
  getters,
};
