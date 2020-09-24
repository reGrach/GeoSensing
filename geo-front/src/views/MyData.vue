<template>
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
</template>

<script>
import MapData from '../components/cards/MapData.vue';
import TableData from '../components/cards/TableData.vue';
export default {
  components: { MapData, TableData },

  data: () => ({
    tab: null,
    points: [
      {
        number: 1,
        author: "Дима",
        x:60.44906,
        y:30.289094,
        coords:[60.44906, 30.289094]
      },
      {
        number: 2,
        author: "Миша",
        x:60.448902,
        y:30.28967,
        coords:[60.448902, 30.28967]
      },
      {
        number: 3,
        author: "Юля",
        x:60.449111,
        y:30.290246,
        coords:[60.449111, 30.290246]
      }
    ]
  }),
};
</script>
