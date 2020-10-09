<template>
  <v-container fluid>
    <v-row align="center" justify="center">
      <v-col cols="1">
        <v-dialog v-model="dialog" persistent max-width="600px">
          <template v-slot:activator="{ on, attrs }">
            <v-btn color="primary" dark v-bind="attrs" v-on="on">
              <v-icon>mdi-plus</v-icon>
            </v-btn>
          </template>
          <create-exp @close="close"></create-exp>
        </v-dialog>
      </v-col>
      <v-col cols="11">
        <v-select
          v-model="currentExp"
          :items="getListExperiments"
          item-value="id"
          item-text="title"
          label="Выберите эксперимент"
          required
          :loading="getProcessing"
        ></v-select>
        <fixations v-if="canFixPoint"></fixations>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import { mapGetters } from "vuex";
import CreateExp from "../components/cards/CreateExp.vue";
import Fixations from "../components/cards/Fixations.vue";
import { GET_LIST_EXP } from "../store/actionsType";

export default {
  components: { Fixations, CreateExp },
  data: () => ({
    dialog: false,
    currentExp: null,
  }),

  computed: {
    ...mapGetters(["isEasyUser", "getProcessing", "getListExperiments"]),
    canFixPoint() {
      return this.currentExp && !this.isEasyUser;
    },
  },

  created() {
    if (!this.isEasyUser) {
      this.$store.dispatch(GET_LIST_EXP);
    }
  },

  methods: {
    close(){
      this.dialog = false
    },
  }
};
</script>
