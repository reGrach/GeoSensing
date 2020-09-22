<template>
  <v-app>
    <app-header></app-header>
    <v-main >
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
import { mapGetters } from 'vuex';

export default {
  components: {
    AppHeader,
    AppFooter,
    Preloader,
  },
  computed: {
    ...mapGetters(['showPreloader']),
  },
};
</script>
