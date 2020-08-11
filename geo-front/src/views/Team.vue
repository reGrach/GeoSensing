<template>
  <init-team v-if="isEasyUser" v-on:enterToTeam="getMyTeam"></init-team>
  <v-container v-else fluid grid-list-xl>
    <v-layout row wrap>
      <v-card-title class="text-h4">{{title}}</v-card-title>
    </v-layout>
    <v-divider></v-divider>
    <v-layout row wrap>
      <v-col cols="12" sm="6" lg="3">
        <widget :color="color" v-model="getDate" icon="mdi-calendar-check" title="Дата создания" />
      </v-col>
      <v-col cols="12" sm="6" lg="3">
        <widget
          :color="color"
          v-model="countParticipants"
          icon="mdi-account-multiple"
          title="Число участников"
        />
      </v-col>
    </v-layout>
    <v-divider></v-divider>
    <list-users :users="users"></list-users>
  </v-container>
</template>

<script>
import { mapGetters } from 'vuex';
import InitTeam from '../components/cards/InitTeam.vue';
import Widget from '../components/cards/Widget.vue';
import ListUsers from '../components/ListUsers.vue';
import { GET_TEAM } from '../store/actionsType';

export default {
  components: { InitTeam, Widget, ListUsers },
  data: () => ({
    title: null,
    color: null,
    createDate: null,
    users: [],
  }),

  mounted() {
    if (!this.isEasyUser) {
      this.getMyTeam();
    }
  },

  computed: {
    ...mapGetters(['isEasyUser']),
    countParticipants() {
      return this.users.length.toString();
    },
    getDate() {
      return new Date(this.createDate).toLocaleString('ru', {
        year: 'numeric',
        month: 'long',
        day: 'numeric',
      });
    },
  },

  methods: {
    getMyTeam() {
      this.$store.dispatch(GET_TEAM).then((data) => {
        this.users = data.participants;
        this.color = data.color;
        this.title = data.title;
        this.createDate = data.createDate;
      });
    },
  },
};
</script>
