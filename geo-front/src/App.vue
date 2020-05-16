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
import AppHeader from '@/components/base/AppHeader'
import AppFooter from '@/components/base/AppFooter'
export default {
  components: {
    AppHeader,
    AppFooter
  },

  data: () => ({
    //
  })
}
</script>
