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
    Vue.axios.defaults.withCredentials = true
    // Vue.axios.interceptors.request.use(request => requestInterceptor)
  },

  //   setHeader () {
  //     Vue.axios.defaults.headers.common.Authorization = `Token ${JwtService.getToken()}`
  //   },

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
