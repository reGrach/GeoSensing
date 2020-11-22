<template>
  <v-app>
    <app-header></app-header>
    <v-main >
      <Notify></Notify>
      <router-view v-show="!showPreloader" />
      <Preloader v-if="showPreloader"></Preloader>
    </v-main>
    <app-footer></app-footer>
  </v-app>
</template>

<script>
import AppHeader from '@/components/base/AppHeader.vue';
import AppFooter from '@/components/base/AppFooter.vue';
import Preloader from '@/components/base/Preloader.vue';
import Notify from '@/components/base/Notify.vue';

import axios from 'axios';
import { mapGetters } from 'vuex';
import { CHECK_AUTH, BAD_REQUEST, FATAL_ERROR } from './store/actionsType';

export default {
  components: {
    AppHeader,
    AppFooter,
    Preloader,
    Notify,
  },
  computed: {
    ...mapGetters(['showPreloader', 'notify']),
  },
  created() {
    axios.interceptors.response.use(undefined, (error) => {
      if (error.response) {
        switch (error.response.status) {
          case 400:
            this.$store.dispatch(BAD_REQUEST, error.response.data);
            break;
          case 401:
            this.$router.push('/signin');
            break;
          case 403:
            this.$store.dispatch(CHECK_AUTH);
            break;
          default:
            this.$store.dispatch(FATAL_ERROR);
            break;
        }
      }
      return Promise.reject(error.response);
    });
  },
};
</script>
