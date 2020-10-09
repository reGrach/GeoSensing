<template>
  <init-team v-if="isEasyUser" v-on:enterToTeam="getMyTeam"></init-team>
  <v-container v-else fluid grid-list-xl>
    <v-layout row wrap>
      <v-card-title class="text-h4">{{ getTeam.title }}</v-card-title>
    </v-layout>
    <v-divider></v-divider>
    <v-layout row wrap>
      <v-col cols="12" sm="6" lg="3">
        <widget
          :color="getTeam.color"
          v-model="getDate"
          icon="mdi-calendar-check"
          title="Дата создания"
        />
      </v-col>
      <v-col cols="12" sm="6" lg="3">
        <widget
          :color="getTeam.color"
          v-model="countParticipants"
          icon="mdi-account-multiple"
          title="Число участников"
        />
      </v-col>
    </v-layout>
    <v-divider></v-divider>
    <list-users :users="getParticipants"></list-users>
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
  data: () => ({}),
  mounted() {
    if (!this.isEasyUser) {
      this.getMyTeam();
    }
  },

  computed: {
    ...mapGetters(['isEasyUser', 'getTeam', 'getParticipants']),
    countParticipants() {
      return this.getParticipants.length.toString();
    },
    getDate() {
      return new Date(this.getTeam.createDate).toLocaleString('ru', {
        year: 'numeric',
        month: 'long',
        day: 'numeric',
      });
    },
  },

  methods: {
    getMyTeam() {
      this.$store.dispatch(GET_TEAM);
    },
  },
};
</script>
