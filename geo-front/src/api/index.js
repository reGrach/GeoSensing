import Vue from 'vue'
import axios from 'axios'
import VueAxios from 'vue-axios'
// import { UN_AUTH } from '../store/actionsType'

const ApiService = {
  init () {
    Vue.use(VueAxios, axios)
    // Vue.axios.defaults.baseURL = 'https://regrach.ru/api/'
    Vue.axios.defaults.baseURL = 'https://localhost:5001/api/'
    Vue.axios.defaults.withCredentials = true
    // Vue.axios.interceptors.response.use(undefined, function (err) {
    //   return new Promise(function (resolve, reject) {
    //     if (err.status === 401 && err.config && !err.config.__isRetryRequest) {
    //       this.store.dispatch(UN_AUTH)
    //     }
    //     throw err
    //   })
    // })
  },

  query (route, params) {
    return Vue.axios.get(route, params)
  },

  get (route) {
    return Vue.axios.get(route)
  },

  post (route, params) {
    return Vue.axios.post(route, params)
  },

  delete (resource) {
    return Vue.axios.delete(resource)
  }
}

export default ApiService
