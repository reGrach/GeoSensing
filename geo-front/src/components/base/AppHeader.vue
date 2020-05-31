<template>
  <div>
    <v-navigation-drawer v-if="showNavigator" app clipped v-model="drawer">
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
        v-if="showNavigator"
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
      <v-toolbar-items v-if="!showNavigator">
        <v-btn text :to="signinItem.route">
          <v-icon v-html="signinItem.icon"></v-icon>
          <span v-html="signinItem.title" class="hidden-sm-and-down"></span>
        </v-btn>
      </v-toolbar-items>
      <v-toolbar-items v-else>
        <v-btn icon large>
          <v-avatar color="primary" item>
            <span class="white--text headline">TT</span>
          </v-avatar>
        </v-btn>
        <v-btn text @click.prevent="signout" :loading="processing">
          <v-icon v-html="signoutItem.icon"></v-icon>
          <span v-html="signoutItem.title" class="hidden-sm-and-down"></span>
        </v-btn>
      </v-toolbar-items>
    </v-app-bar>
  </div>
</template>

<script>
import { SIGN_OUT } from '@/store/actionsType'

export default {
  props: {
    menu: Array
  },
  data: () => ({
    drawer: true,
    title: 'GeoSensing',
    menuItems: [
      {
        name: 'home',
        icon: 'mdi-home',
        title: 'Домашняя страница',
        route: '/',
        isNavigate: true
      },
      {
        name: 'main',
        icon: 'mdi-home',
        title: 'Главная',
        route: '/main',
        isNavigate: true
      },
      {
        name: 'login',
        icon: 'mdi-login',
        title: '&nbsp; Войти в систему',
        route: '/signin',
        isNavigate: false
      },
      {
        name: 'logout',
        icon: 'mdi-logout',
        title: '&nbsp; Выйти',
        isNavigate: false
      }
    ]
  }),
  computed: {
    navigateMenu () {
      return this.menuItems.filter(el => el.isNavigate)
    },
    signinItem () {
      return this.menuItems.find(item => item.name === 'login')
    },
    signoutItem () {
      return this.menuItems.find(item => item.name === 'logout')
    },
    showNavigator () {
      return this.$store.getters.isAuthenticated
    },
    processing () {
      return this.$store.getters.getProcessing
    }
  },
  methods: {
    signout () {
      this.$store
        .dispatch(SIGN_OUT)
        .then(() => this.$router.push({ name: 'signin' }))
    }
  }
}
</script>
