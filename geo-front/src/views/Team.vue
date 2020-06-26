<template>
<<<<<<< HEAD
  <init-team v-if="isEasyUser"></init-team>
  <div v-else>В КОМАНДЕ</div>
=======
  <init-team v-if="isEasyUser" v-on:enterToTeam="getMyTeam"></init-team>
  <v-container v-else fluid grid-list-xl>
    <v-layout row wrap>
      <!-- Widgets-->
      <v-flex d-flex lg3 sm6 xs12>
        <widget
          icon="mdi-home"
          title="1,287,687"
          subTitle="13% higher yesterday"
          supTitle="Today's Visits"
          color="#00b297"
        />
      </v-flex>
      <v-flex d-flex lg3 sm6 xs12>
        <widget
          icon="mdi-home"
          title="$141,291"
          subTitle="$117,212 before tax"
          supTitle="Today's Sales"
          color="#dc3545"
        />
      </v-flex>
      <v-flex d-flex lg3 sm6 xs12>
        <widget
          icon="mdi-home"
          title="33.45%"
          subTitle="13% average duration"
          supTitle="% Unique Visits"
          color="#0866C6"
        />
      </v-flex>
      <v-flex d-flex lg3 sm6 xs12>
        <widget
          icon="mdi-home"
          title="13.00%"
          subTitle="17.25% on average time"
          supTitle="Bounce Rate"
          color="#1D2939"
        />
      </v-flex>
    </v-layout>

    <v-divider></v-divider>
    <list-users :users="users"></list-users>
  </v-container>
>>>>>>> develop
</template>

<script>
import { mapGetters } from 'vuex';
import InitTeam from '../components/cards/InitTeam.vue';
<<<<<<< HEAD

export default {
  components: { InitTeam },
  data: () => ({}),
=======
import Widget from '../components/cards/Widget.vue';
import ListUsers from '../components/ListUsers.vue';

import { GET_TEAM } from '../store/actionsType';


export default {
  components: { InitTeam, Widget, ListUsers },
  data: () => ({
    users: [],
  }),

  mounted() {
    if (!this.isEasyUser) {
      this.getMyTeam();
    }
  },
>>>>>>> develop

  computed: {
    ...mapGetters(['isEasyUser']),
  },

<<<<<<< HEAD
  methods: {},
=======
  methods: {
    getMyTeam() {
      this.$store.dispatch(GET_TEAM)
        .then((data) => {
          this.users = data.participants;
        });
    },
  },
>>>>>>> develop
};
</script>
