import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '@/views/Home.vue'
import Signup from '@/views/Signup.vue'
import Signin from '@/views/Signin.vue'
import Main from '@/views/Main.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: '/signin',
    name: 'Вход',
    component: Signin
  },
  {
    path: '/signup',
    name: 'Регистрация',
    component: Signup
  },
  {
    path: '/main',
    name: 'Главная',
    component: Main
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
