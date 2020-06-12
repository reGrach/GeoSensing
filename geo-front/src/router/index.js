import Vue from 'vue';
import VueRouter from 'vue-router';
import store from '../store/index';

Vue.use(VueRouter);

function AuthGuard(from, to, next) {
  if (store.getters.isAuthenticated) { next(); } else { next('/signin'); }
}

function AdminGuard(from, to, next) {
  if (store.getters.isAuthenticated && store.getters.isAdmin) { next(); } else { next('/'); }
}

function ifAuthenticated(from, to, next) {
  if (store.getters.isAuthenticated) { next('/'); } else { next(); }
}

const routes = [
  {
    path: '/',
    name: 'Домашняя',
    component: () => import('@/views/Home'),
    beforeEnter: AuthGuard,
    children: [
      {
        path: '',
        name: 'Главная',
        component: () => import('@/views/Main'),
      },
      {
        path: 'maphelp',
        name: 'Карта',
        component: () => import('@/views/Map'),
      },
    ],
  },
  {
    path: '/map',
    name: 'Карта',
    component: () => import('@/views/Map'),
    beforeEnter: AuthGuard,
  },
  {
    path: '/signin',
    name: 'Вход',
    component: () => import('@/views/Signin'),
    beforeEnter: ifAuthenticated,
  },
  {
    path: '/signup',
    name: 'Регистрация',
    component: () => import('@/views/Signup'),
    beforeEnter: ifAuthenticated,
  },
  {
    path: '/Profile',
    name: 'Мой профиль',
    component: () => import('@/views/Profile'),
    beforeEnter: AuthGuard,
  },
  {
    path: '/Admin',
    name: 'Страница администратора',
    component: () => import('@/views/Profile'),
    beforeEnter: AdminGuard,
  },
];

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes,
});

export default router;
