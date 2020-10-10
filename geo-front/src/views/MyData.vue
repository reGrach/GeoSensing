<template>
  <v-container fluid>
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
        <v-tabs-items v-model="tab">
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
import { mapGetters } from 'vuex';
import MapData from '../components/cards/MapData.vue';
import TableData from '../components/cards/TableData.vue';
import { GET_LIST_EXP } from '../store/actionsType';

export default {
  components: { MapData, TableData },

  data: () => ({
    tab: null,
    currentExp: null,
    points: [
      {
        number: 1,
        author: 'Дима',
        x: 60.44906,
        y: 30.289094,
        coords: [60.44906, 30.289094],
      },
      {
        number: 2,
        author: 'Миша',
        x: 60.448902,
        y: 30.28967,
        coords: [60.448902, 30.28967],
      },
      {
        number: 3,
        author: 'Юля',
        x: 60.449111,
        y: 30.290246,
        coords: [60.449111, 30.290246],
      },
    ],
  }),
  computed: {
    ...mapGetters(['isEasyUser', 'getProcessing', 'getListExperiments']),
    canShowMap() {
      return this.getListExperiments.length > 0 && !this.isEasyUser;
    },
  },
  created() {
    if (!this.isEasyUser) {
      this.$store.dispatch(GET_LIST_EXP);
    }
  },
};
</script>
