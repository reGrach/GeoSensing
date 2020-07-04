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
        <v-menu rounded="lg" offset-y>
          <template v-slot:activator="{ on }">
            <v-btn v-on="on" icon>
              <v-avatar color="primary" item>
                <v-img v-if="useAvatarImg" :src="getAvatar"></v-img>
                <span v-else class="white--text headline">{{getLoginToIcon}}</span>
              </v-avatar>
            </v-btn>
          </template>
          <v-list>
            <v-list-item to="/profile">
              <v-list-item-icon>
                <v-icon>mdi-account</v-icon>
              </v-list-item-icon>
              <v-list-item-content>
                <v-list-item-title>Профиль</v-list-item-title>
              </v-list-item-content>
            </v-list-item>
            <v-list-item @click.prevent="signout">
              <v-list-item-icon>
                <v-icon>mdi-logout</v-icon>
              </v-list-item-icon>
              <v-list-item-content>
                <v-list-item-title>Выйти</v-list-item-title>
              </v-list-item-content>
            </v-list-item>
          </v-list>
        </v-menu>
      </div>
    </v-app-bar>
  </div>
</template>

<script>
import { SIGN_OUT } from '@/store/actionsType';
import { mapGetters } from 'vuex';
import { menu, adminMenu } from '../../common/navigateMenu';

export default {
  props: {
    menu: Array,
  },
  data: () => ({
    drawer: true,
    title: 'GeoSensing',
  }),
  computed: {
    ...mapGetters([
      'currentLogin',
      'isAuthenticated',
      'getProcessing',
      'getAvatar',
      'isAdmin',
    ]),
    useAvatarImg() {
      return !!this.getAvatar;
    },
    navigateMenu() {
      return this.isAdmin ? adminMenu : menu;
    },
    getLoginToIcon() {
      return this.currentLogin[0];
    },
  },
  methods: {
    signout() {
      this.$store.dispatch(SIGN_OUT).then(() => this.$router.push('/signin'));
    },
  },
};
</script>
<style scoped>
.v-btn.no-active::before {
  opacity: 0 !important;
}
</style>
