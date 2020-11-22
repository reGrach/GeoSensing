<template>
  <v-container fluid>
        <v-btn color="success" class="mr-0" @click="check">Проверка</v-btn>

      'Ошибка!!!'
  </v-container>
</template>

<script>
import { GET_PROFILE } from '@/store/actionsType';

export default {

  methods: {
    check() {
      this.$store.dispatch(GET_PROFILE, 1).then((data) => {
        console.log(data);
      });
    },
  },
};
</script>
