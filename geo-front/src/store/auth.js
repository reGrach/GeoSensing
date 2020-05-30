import AuthApi from '../api/auth'
import { SIGN_IN, SIGN_UP, SIGN_OUT } from './actionsType'
import { SET_AUTH, PURGE_AUTH, SET_ERROR } from './mutationsType'

const state = {
  userId: null,
  isAuthenticated: false,
  errors: null
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
  [SIGN_IN] (context, credentials) {
    return new Promise(resolve => {
      AuthApi.signin(credentials)
        .then(({ data }) => {
          context.commit(SET_AUTH)
          resolve(data)
        })
        .catch(({ response }) => {
          console.log('---------------')
          console.log(response)
          // context.commit(SET_ERROR, response.data.errors)
        })
    })
  },
  [SIGN_UP] (context, credentials) {
    return new Promise((resolve, reject) => {
      AuthApi.signup(credentials)
        .then(({ data }) => {
          context.commit(SET_AUTH)
          resolve(data)
        })
        .catch(({ response }) => {
          context.commit(SET_ERROR, response.data.errors)
          reject(response)
        })
    })
  },
  [SIGN_OUT] (context) {
    context.commit(PURGE_AUTH)
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
  [SET_ERROR] (state, error) {
    state.errors = error
  },
  [SET_AUTH] (state, user) {
    state.isAuthenticated = true
    // state.userId = user
    state.errors = {}
    // JwtService.saveToken(state.user.token)
  },
  [PURGE_AUTH] (state) {
    state.isAuthenticated = false
    state.userId = {}
    state.errors = {}
    // JwtService.destroyToken()
  }
}

export default {
  state,
  actions,
  mutations,
  getters
}
