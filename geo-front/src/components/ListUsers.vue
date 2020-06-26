<template>
  <v-list subheader>
    <v-subheader>Участники команды</v-subheader>

    <v-list-item v-for="user in users" :key="user.login">
      <v-list-item-icon>
        <v-badge overlap icon="mdi-star" :value="user.isLeader">
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
      </v-list-item-content>

      <v-list-item-icon>
        <v-icon :color="user.isLeader ? 'deep-purple accent-4' : 'grey'">mdi-chat</v-icon>
      </v-list-item-icon>
    </v-list-item>
  </v-list>
</template>

<script>
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
};
</script>
