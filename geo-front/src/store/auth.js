import AuthApi from '../api/auth'
import { SIGN_IN, SIGN_UP, SIGN_OUT } from './actionsType'
import { SET_AUTH, PURGE_AUTH, SET_ERROR, SET_PROCESSING } from './mutationsType'

const state = {
  userId: null,
  isAuthenticated: false
}

const getters = {
  currentUserId (state) {
    return state.userId
  },
  isAuthenticated (state) {
    return state.isAuthenticated
  }
}

const actions = {
  [SIGN_IN] ({ commit }, credentials) {
    commit(SET_PROCESSING, true)
    return new Promise(resolve => {
      AuthApi.signin(credentials)
        .then(({ data }) => {
          commit(SET_AUTH)
          resolve(data)
        })
        .catch(({ response }) => {
          if (response.status === 401) {
            commit(SET_ERROR, response.data)
          } else {
            console.error('Фатальная ошибка')
            console.log(response.data)
            commit(SET_ERROR, response.data)
          }
        })
        .finally(() => {
          commit(SET_PROCESSING, false)
        })
    })
  },
  [SIGN_UP] ({ commit }, credentials) {
    commit(SET_PROCESSING, true)
    return new Promise((resolve, reject) => {
      AuthApi.signup(credentials)
        .then(({ data }) => {
          resolve(data)
        })
        .catch(({ response }) => {
          if (response.status === 401) {
            commit(SET_ERROR, response.data)
          } else {
            console.error('Фатальная ошибка')
            console.log(response.data)
            commit(SET_ERROR, response.data)
          }
          reject(response)
        })
        .finally(() => {
          commit(SET_PROCESSING, false)
        })
    })
  },
  [SIGN_OUT] ({ commit }, context) {
    return new Promise((resolve, reject) => {
      AuthApi.signout()
        .then(({ data }) => {
          commit(PURGE_AUTH)
          resolve(data)
        })
        .catch(({ response }) => {
          if (response.status === 401) {
            commit(SET_ERROR, response.data)
          } else {
            console.error('Фатальная ошибка')
            console.log(response.data)
            commit(SET_ERROR, response.data)
          }
          reject(response)
        })
        .finally(() => {
          commit(SET_PROCESSING, false)
        })
    })
  }
  // [CHECK_AUTH] (context) {
  //   if (JwtService.getToken()) {
  //     ApiService.setHeader()
  //     ApiService.get('user')
  //       .then(({ data }) => {
  //         context.commit(SET_AUTH, data.user)
  //       })
  //       .catch(({ response }) => {
  //         context.commit(SET_ERROR, response.data.errors)
  //       })
  //   } else {
  //     context.commit(PURGE_AUTH)
  //   }
  // },
}

const mutations = {
  [SET_AUTH] (state, user) {
    state.isAuthenticated = true
    // state.userId = user
    // JwtService.saveToken(state.user.token)
  },
  [PURGE_AUTH] (state) {
    state.isAuthenticated = false
    // state.userId = {}
    // JwtService.destroyToken()
  }
}

export default {
  state,
  actions,
  mutations,
  getters
}
