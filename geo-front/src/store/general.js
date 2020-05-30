export default {
  state: {
    processing: false,
    error: null
  },
  mutations: {
    SET_PROCESSING (state, payload) {
      state.processing = payload
    },
    SET_ERROR (state, payload) {
      state.error = payload
    },
    CLEAN_ERROR (state, payload) {
      state.error = null
    }
  },
  getters: {
    getProcessing: (state) => state.processing,
    getError: (state) => state.error
  }
}
