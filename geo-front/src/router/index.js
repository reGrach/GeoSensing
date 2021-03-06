import Vue from 'vue';
import VueRouter from 'vue-router';
import store from '../store/index';
import { CLEAR_ALL } from '../store/mutationsType';

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
    path: '*',
    redirect: '/data',
  },
  {
    path: '/error',
    name: 'Ошибка',
    component: () => import('@/views/ErrorPage'),
  },
  {
    path: '/experiment',
    name: 'Эксперимент',
    component: () => import('@/views/Experiment'),
    beforeEnter: AuthGuard,
  },
  {
    path: '/data',
    name: 'Мои данные',
    component: () => import('@/views/MyData'),
    beforeEnter: AuthGuard,
  },
  {
    path: '/team',
    name: 'Команда',
    component: () => import('@/views/Team'),
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
    name: 'Админка',
    component: () => import('@/views/Admin/Admin'),
    beforeEnter: AdminGuard,
  },
];

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes,
});

router.beforeEach((to, from, next) => {
  store.commit(CLEAR_ALL);
  next();
});

export default router;
