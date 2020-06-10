<template>
  <div>
    <v-navigation-drawer v-if="isAuthenticated" app clipped v-model="drawer">
      <v-list dense>
        <v-list-item
          v-for="(item, i) in navigateMenu"
          text
          :key="`navigateMenu${i}`"
          :to="item.route"
        >
          <v-list-item-action>
            <v-icon v-html="item.icon"></v-icon>
          </v-list-item-action>
          <v-list-item-content>
            <v-list-item-title v-text="item.title"></v-list-item-title>
          </v-list-item-content>
        </v-list-item>
      </v-list>
    </v-navigation-drawer>
    <v-app-bar app clipped-left color="indigo" dark>
      <v-app-bar-nav-icon
        v-if="isAuthenticated"
        @click.stop="drawer = !drawer"
        class="hidden-lg-and-up"
      />
      <router-link to="/" tag="div" style="cursor:pointer">
        <v-toolbar-title class="headline font-weight-black">
          <v-icon x-large>$geo</v-icon>
          {{title}}
        </v-toolbar-title>
      </router-link>
      <v-spacer></v-spacer>
      <v-toolbar-items v-if="!isAuthenticated">
        <v-btn text to="/signin" active-class="no-active">
          <v-icon>mdi-login</v-icon>
          <span v-html="'&nbsp; Войти в систему'" class="hidden-sm-and-down"></span>
        </v-btn>
      </v-toolbar-items>
      <div v-else>
        <v-btn icon large to="/profile" active-class="no-active">
          <v-avatar color="primary" item>
            <span class="white--text headline">{{getLoginToIcon}}</span>
          </v-avatar>
        </v-btn>
        <v-btn text bottom @click.prevent="signout" :loading="getProcessing">
          <v-icon>mdi-logout</v-icon>
          <span v-html="'&nbsp; Выйти'" class="hidden-sm-and-down"></span>
        </v-btn>
      </div>
    </v-app-bar>
  </div>
</template>

<script>
import { SIGN_OUT } from '@/store/actionsType'
import { mapGetters } from 'vuex'

export default {
  props: {
    menu: Array
  },
  data: () => ({
    drawer: true,
    title: 'GeoSensing'
  }),
  computed: {
    ...mapGetters(['currentLogin', 'isAuthenticated', 'getProcessing']),
    navigateMenu () {
      return [
        {
          name: 'main',
          icon: 'mdi-home',
          title: 'Главная',
          vetr_features
          route: '/'
        },
        {
          name: 'main',
          icon: 'mdi-map',
          title: 'Карта',
          route: '/map'
        }
      ]
    },
    getLoginToIcon () {
      return this.currentLogin[0]
    }
  },
  methods: {
    signout () {
      this.$store
        .dispatch(SIGN_OUT)
        .then(() => this.$router.push('/signin'))
    }
  }
}
</script>
<style scoped>
.v-btn--active.no-active::before {
  opacity: 0 !important;
}
</style>
