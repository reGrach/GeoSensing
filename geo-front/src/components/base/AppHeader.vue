<template>
  <v-app-bar app color="indigo" dark>
    <v-app-bar-nav-icon @click.stop="drawer = !drawer" class="hidden-md-and-up" />
    <v-icon x-large >$geo</v-icon>
    <v-toolbar-title class="headline font-weight-black">{{title}}</v-toolbar-title>
    <v-spacer></v-spacer>
    <v-toolbar-items class="hidden-sm-and-down">
      <v-btn v-for="(item, i) in menuItems" text :key="`menuItems${i}`" :to="item.route">
        {{item.title}}
      </v-btn>
    </v-toolbar-items>
  </v-app-bar>
</template>

<script>
export default {
  data: () => ({
    drawer: false,
    title: 'GeoSensing'
  }),
  computed: {
    menuItems () {
      return [
        {
          icon: 'mdi-home',
          title: 'Домашняя страница',
          route: '/'
        },
        {
          icon: 'mdi-home',
          title: 'Регистрация',
          route: '/signup'
        },
        {
          icon: 'mdi-home',
          title: 'Вход',
          route: '/signin'
        }
      ]
    }
  }
}
</script>
