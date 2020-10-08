<template>
  <v-card>
    <v-card-title>
      <span class="headline">Создать эксперимент</span>
    </v-card-title>
    <v-card-text>
      <v-container>
        <v-row>
          <v-text-field label="Название эксперимента" required></v-text-field>
        </v-row>
      </v-container>
    </v-card-text>
    <v-card-actions>
      <v-spacer></v-spacer>
      <v-btn color="blue darken-1" text @click="closeDialog">
        Закрыть
      </v-btn>
      <v-btn color="blue darken-1" text @click="closeDialog">
        Сохранить
      </v-btn>
    </v-card-actions>
  </v-card>
</template>

<script>
export default {
  data: () => ({
  }),
  methods: {
    closeDialog(){
      this.$emit('close');
    }
  }
};
</script>