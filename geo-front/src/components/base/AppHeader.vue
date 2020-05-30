<template>
  <div>
    <v-navigation-drawer app clipped v-model="drawer">
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
      <v-app-bar-nav-icon @click.stop="drawer = !drawer" class="hidden-lg-and-up" />
      <router-link to="/" tag="div" style="cursor:pointer">
        <v-toolbar-title class="headline font-weight-black">
          <v-icon x-large>$geo</v-icon>
          {{title}}
        </v-toolbar-title>
      </router-link>
      <v-spacer></v-spacer>
      <v-toolbar-items>
        <v-btn text v-for="(item, i) in authMenu" :key="`authMenu${i}`" :to="item.route">
          <v-icon v-html="item.icon"></v-icon>
          <span v-html="item.title" class="hidden-sm-and-down"></span>
        </v-btn>
      </v-toolbar-items>
    </v-app-bar>
  </div>
</template>

<script>
export default {
  props: {
    menu: Array
  },
  data: () => ({
    drawer: true,
    title: 'GeoSensing',
    menuItems: [
      {
        icon: 'mdi-home',
        title: 'Домашняя страница',
        route: '/',
        isNavigate: true
      },
      {
        icon: 'mdi-home',
        title: 'Главная',
        route: '/main',
        isNavigate: true
      },
      {
        icon: 'mdi-login',
        title: '&nbsp; Войти в систему',
        route: '/signin',
        isNavigate: false
      }
    ]
  }),
  computed: {
    navigateMenu () {
      return this.menuItems.filter(el => el.isNavigate)
    },
    authMenu () {
      return this.menuItems.filter(el => !el.isNavigate)
    }
  }
}
</script>
