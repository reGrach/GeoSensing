<template>
  <v-container fluid>
    <v-select
      v-model="currentExp"
      :items="expirement"
      item-value="id"
      item-text="title"
      label="Выберите эксперимент"
      required
      :loading="getProcessing"
    ></v-select>
      <fixations v-if="canFixPoint"></fixations>
  </v-container>
</template>

<script>
import { mapGetters } from 'vuex';
import Fixations from '../components/cards/Fixations.vue';
import { GET_LIST_EXP } from '../store/actionsType';

export default {
  components: { Fixations },
  data: () => ({
    expirement: [],
    currentExp: null,
  }),

  computed: {
    ...mapGetters(['isEasyUser', 'getProcessing']),
    canFixPoint() {
      return this.expirement.length > 0 && !this.isEasyUser;
    },
  },

  created() {
    if (!this.isEasyUser) {
      this.$store.dispatch(GET_LIST_EXP).then((list) => {
        this.expirement = list;
      });
    }
  },
};
</script>
