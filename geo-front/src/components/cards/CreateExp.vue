<template>
  <v-card>
    <v-card-title>
      <span class="headline">Создать эксперимент</span>
    </v-card-title>
    <v-card-text>
      <v-container>
        <v-row>
          <v-text-field
            label="Название эксперимента"
            v-model="title"
            required
          ></v-text-field>
        </v-row>
      </v-container>
    </v-card-text>
    <v-card-actions>
      <v-spacer></v-spacer>
      <v-btn color="blue darken-1" text @click="closeDialog"> Закрыть </v-btn>
      <v-btn color="blue darken-1" text @click="createExp"> Создать </v-btn>
    </v-card-actions>
  </v-card>
</template>

<script>
import { NEW_EXP } from '@/store/actionsType';

export default {
  data: () => ({
    title: '',
  }),
  methods: {
    closeDialog() {
      this.$emit('close');
    },
    createExp() {
      console.log(this.title);
      this.$store
        .dispatch(NEW_EXP, this.title)
        .then(this.closeDialog, (this.title = ''));
    },
  },
};
</script>
