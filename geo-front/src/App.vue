<template>
  <v-app>
    <app-header></app-header>
    <v-main>
      <router-view/>
    </v-main>
    <app-footer></app-footer>
  </v-app>
</template>

<script>
import AppHeader from '@/components/base/AppHeader.vue';
import AppFooter from '@/components/base/AppFooter.vue';

export default {
  components: {
    AppHeader,
    AppFooter,
  },
};
</script>
