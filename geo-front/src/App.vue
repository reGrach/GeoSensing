<template>
  <v-app>
    <app-header></app-header>
    <v-content>
      <router-view/>
    </v-content>
    <app-footer></app-footer>
  </v-app>
</template>

<script>
import AppHeader from '@/components/base/AppHeader.vue';
import AppFooter from '@/components/base/AppFooter.vue';
import axios from 'axios';
import { PURGE_AUTH } from './store/mutationsType';

export default {
  components: {
    AppHeader,
    AppFooter,
  },
  created() {
    axios.interceptors.response.use(
      (response) => response,
      (error) => {
        const { status } = error.response;
        if (status === 401) {
          this.$store.commit(PURGE_AUTH);
        }
        return Promise.reject(error);
      },
    );
  },
};
</script>
