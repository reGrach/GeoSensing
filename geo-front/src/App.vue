<template>
  <v-app id="main_layout">
    <v-navigation-drawer v-model="drawer" app>
      <v-list dense>
        <v-list-item link to="/">
          <v-list-item-action>
            <v-icon>mdi-home</v-icon>
          </v-list-item-action>
          <v-list-item-content>
            <v-list-item-title>Мои данные</v-list-item-title>
          </v-list-item-content>
        </v-list-item>

        <v-list-item link to="/geo">
          <v-list-item-action>
            <v-icon>mdi-contact-mail</v-icon>
          </v-list-item-action>
          <v-list-item-content>
            <v-list-item-title>Фиксация позиции</v-list-item-title>
          </v-list-item-content>
        </v-list-item>

        <v-list-item link to="/about">
          <v-list-item-action>
            <v-icon>mdi-contact-mail</v-icon>
          </v-list-item-action>
          <v-list-item-content>
            <v-list-item-title>О проекте</v-list-item-title>
          </v-list-item-content>
        </v-list-item>
      </v-list>
    </v-navigation-drawer>

    <v-app-bar app color="indigo" dark>
      <v-app-bar-nav-icon @click.stop="drawer = !drawer" />
      <v-toolbar-title>{{currentRouteName}}</v-toolbar-title>
    </v-app-bar>

    <v-content>
      <router-view />
    </v-content>

    <v-footer color="indigo" app>
      <span class="white--text">&copy;GeoSensing - 2020 / Все права защищены, отвечаю!</span>
    </v-footer>
  </v-app>
</template>

<script>
export default {
  props: {
    source: String
  },

  data: () => ({
    drawer: null
  }),

  computed: {
    currentRouteName() {
      return this.$route.name;
    }
  }
};
</script>
