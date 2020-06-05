import Vue from 'vue'
import VueRouter from 'vue-router'
import Signup from '@/views/Signup.vue'
import Signin from '@/views/Signin.vue'
import Main from '@/views/Main.vue'
import store from '../store/index'

Vue.use(VueRouter)

function AuthGuard (from, to, next) {
  if (store.getters.isAuthenticated) { next() } else { next('/signin') }
}

function ifAuthenticated (from, to, next) {
  if (store.getters.isAuthenticated) { next('/') } else { next() }
}

const routes = [
  {
    path: '/',
    name: 'Главная',
    component: Main,
    beforeEnter: AuthGuard
  },
  {
    path: '/signin',
    name: 'Вход',
    component: Signin,
    beforeEnter: ifAuthenticated
  },
  {
    path: '/signup',
    name: 'Регистрация',
    component: Signup,
    beforeEnter: ifAuthenticated
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
