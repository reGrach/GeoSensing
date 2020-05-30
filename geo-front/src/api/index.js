import Vue from 'vue'
import axios from 'axios'
import VueAxios from 'vue-axios'

// const requestInterceptor = (request) => {
//   request.withCredentials = true
//   return request
// }

const ApiService = {
  init () {
    Vue.use(VueAxios, axios)
    Vue.axios.defaults.baseURL = 'https:/regrach.ru/api/'
    // Vue.axios.interceptors.request.use(request => requestInterceptor)
    Vue.axios.request.withCredentials = true
  },

  //   setHeader () {
  //     Vue.axios.defaults.headers.common.Authorization = `Token ${JwtService.getToken()}`
  //   },

  query (route, params) {
    return Vue.axios.get(route, params).catch(error => {
      throw new Error(`[RWV] ApiService ${error}`)
    })
  },

  get (route) {
    return Vue.axios.get(route).catch(error => {
      throw new Error(`[RWV] ApiService ${error}`)
    })
  },

  post (route, params) {
    return Vue.axios.post(route, params)
  },

  delete (resource) {
    return Vue.axios.delete(resource).catch(error => {
      throw new Error(`[RWV] ApiService ${error}`)
    })
  }
}

export default ApiService
