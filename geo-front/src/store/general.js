import { SET_ERROR, PURGE_ERROR, SET_PROCESSING } from './mutationsType'

const state = {
  processing: false,
  error: null
}

const mutations = {
  [SET_ERROR] (state, payload) {
    state.error = payload
  },
  [PURGE_ERROR] (state) {
    state.error = null
  },
  [SET_PROCESSING] (state, payload) {
    state.processing = payload
  }
}

const getters = {
  getProcessing: (state) => state.processing,
  getError: (state) => state.error
}

export default {
  state,
  mutations,
  getters
}
