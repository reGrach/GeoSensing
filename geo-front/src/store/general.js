import { SET_ERROR, PURGE_ERROR, SET_PROCESSING } from './mutationsType'

export default {
  state: {
    processing: false,
    error: null
  },
  mutations: {
    [SET_ERROR] (state, payload) {
      state.error = payload
    },
    [PURGE_ERROR] (state, payload) {
      state.error = null
    },
    [SET_PROCESSING] (state, payload) {
      state.processing = payload
    }
  },
  getters: {
    getProcessing: (state) => state.processing,
    getError: (state) => state.error
  }
}
