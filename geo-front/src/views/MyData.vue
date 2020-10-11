<template>
  <v-container>
    <h1 v-if="!canShowMap">Вы никто</h1>
    <div v-else>
      <v-select
        v-model="currentExp"
        :items="getListExperiments"
        item-value="id"
        item-text="title"
        label="Выберите эксперимент"
        required
        :loading="getProcessing"
      ></v-select>

      <v-card tile class="elevation-0">
        <v-tabs v-model="tab" color="indigo" flat grow>
          <v-tab>Карта</v-tab>
          <v-tab>Таблица</v-tab>
        </v-tabs>
        <v-tabs-items v-model="tab" touchless>
          <v-tab-item>
            <MapData :points="points"> </MapData>
          </v-tab-item>
          <v-tab-item>
            <TableData :points="points"> </TableData>
          </v-tab-item>
        </v-tabs-items>
      </v-card>
    </div>
  </v-container>
</template>

<script>
import MapData from "../components/cards/MapData.vue";
import TableData from "../components/cards/TableData.vue";
import { GET_LIST_EXP } from "../store/actionsType";
import { GET_LIST_POINTS } from "../store/actionsType";
import { mapGetters } from "vuex";
export default {
  components: { MapData, TableData },

  data: () => ({
    tab: null,
    currentExp: null,
    experimentsPoints: [],
  }),
  computed: {
    ...mapGetters(["isEasyUser", "getProcessing", "getListExperiments"]),
    canShowMap() {
      return this.getListExperiments.length > 0 && !this.isEasyUser;
    },
    points() {
      let points = [];
      this.experimentsPoints.forEach((item) => {
        if (item.experimentId == this.currentExp) {
          points = item.points;
        }
      });
      return points;
    },
  },
  created() {
    if (!this.isEasyUser) {
      this.$store.dispatch(GET_LIST_EXP);
      this.$store.dispatch(GET_LIST_POINTS).then((data) => {
        this.experimentsPoints = data.listPoint;
      });
    }
  },

  methods: {
    // changeExp() {
    //   this.experimentsPoints.forEach((item) => {
    //     if (item.experimentId == this.currentExp) {
    //       return item.points;
    //     }
    //   });
    // }
  },
};
</script>
