<template>
  <v-list subheader>
    <v-subheader>Участники команды</v-subheader>

    <v-list-item v-for="user in users" :key="user.login">
      <v-list-item-icon>
        <v-badge
          overlap
          icon="mdi-star"
          color="deep-purple accent-4"
          :value="user.isLeader"
        >
          <v-avatar>
            <v-img :src="user.avatarSrc">
              <template v-slot:placeholder>
                <v-img :src="placeholder"></v-img>
              </template>
            </v-img>
          </v-avatar>
        </v-badge>
      </v-list-item-icon>

      <v-list-item-content>
        <v-list-item-title v-text="user.login"></v-list-item-title>
        <v-list-item-action-text
          v-text="getName(user.surName, user.name)"
        ></v-list-item-action-text>
      </v-list-item-content>

      <v-list-item-icon>
        <v-btn
          :disabled="canRemove(user.isLeader, user.login)"
          icon
          @click.prevent="removeUser(user.login)"
        >
          <v-icon color="red">mdi-account-cancel</v-icon>
        </v-btn>
      </v-list-item-icon>
    </v-list-item>
  </v-list>
</template>

<script>
import { mapGetters } from 'vuex';
import { LEAVE_TEAM } from '@/store/actionsType';

import placeholder from '../assets/placeholder.png';

export default {
  props: {
    users: {
      type: Array,
      required: true,
    },
  },
  data: () => ({
    placeholder,
  }),

  computed: {
    ...mapGetters(['currentLogin', 'isLeader']),
  },

  methods: {
    getName(surName, name) {
      return surName || name ? `${surName} ${name}` : '';
    },
    canRemove(isLeader, login) {
      if (this.isLeader) {
        return isLeader;
      }
      return this.currentLogin !== login;
    },
    removeUser(login) {
      const userLogin = this.isLeader ? login : null;
      this.$store.dispatch(LEAVE_TEAM, userLogin)
        .then(() => this.$router.go());
    },
  },
};
</script>
