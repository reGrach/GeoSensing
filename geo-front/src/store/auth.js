import AuthApi from '../api/auth'
import { SIGN_IN, SIGN_UP, SIGN_OUT } from './actionsType'
import { CHECK_AUTH, SET_AUTH, PURGE_AUTH, SET_ERROR, SET_PROCESSING } from './mutationsType'
import { getLogin, saveLogin } from '../common/localStorage'

const state = {
  userLogin: null,
  isAuthenticated: false
}

const actions = {
  [SIGN_IN] ({ commit }, credentials) {
    commit(SET_PROCESSING, true)
    return new Promise(resolve => {
      AuthApi.signin(credentials)
        .then(({ data }) => {
          commit(SET_AUTH, data)
          resolve(data)
        })
        .catch(({ response }) => {
          // if (response.status === 401) {
          //   commit(SET_ERROR, response.data)
          // } else {
          //   console.error('Фатальная ошибка')
          console.log(response.data)
          commit(SET_ERROR, response.data)
          // }
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
          console.log(response)
          commit(SET_ERROR, response.data)
          // if (response.status === 401) {
          //   commit(SET_ERROR, response.data)
          // } else {
          //   console.error('Фатальная ошибка')
          //   console.log(response.data)
          //   commit(SET_ERROR, response.data)
          // }
          reject(response)
        })
        .finally(() => {
          commit(SET_PROCESSING, false)
        })
    })
  },

  [CHECK_AUTH] ({ commit }) {
    return new Promise(resolve => {
      AuthApi.check()
        .then(({ data }) => {
          commit(SET_AUTH, data)
          resolve(data)
        })
        .catch(({ response }) => {
          console.log(response)
        })
    })
  }
}

const mutations = {
  [SET_AUTH] (state, { login, expirationDate }) {
    saveLogin(login, expirationDate)
    state.isAuthenticated = true
    state.userLogin = login
  },
  [PURGE_AUTH] (state) {
    state.isAuthenticated = false
    state.userLogin = null
  },
  [CHECK_AUTH] (state) {
    const login = getLogin()
    if (login) {
      state.isAuthenticated = true
      state.userLogin = login
    }
  }
}

const getters = {
  currentLogin (state) {
    return state.userLogin
  },
  isAuthenticated (state) {
    return state.isAuthenticated
  }
}

export default {
  state,
  actions,
  mutations,
  getters
}
