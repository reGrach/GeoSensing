import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '@/views/Home.vue'
import Authentication from '@/views/Authentication'
import Geo from '@/views/GeoNew.vue'
import About from '@/views/About.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'Мои данные',
    component: Home
  },
  {
    path: '/geo',
    name: 'Фиксация позиции',
    component: Geo
  },
  {
    path: '/login',
    name: 'Authentication',
    component: Authentication
  },
  {
    path: '/about',
    name: 'О проекте',
    component: About
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
