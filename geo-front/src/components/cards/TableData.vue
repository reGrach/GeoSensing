<template>
  <div>
    <v-data-table
      dense
      :headers="headers"
      :items="points"
      item-key="name"
      class="elevation-1"
    ></v-data-table>
  </div>
</template>

<script>
export default {
  props: {
    points: Array,
  },
  data: () => ({
    headers: [
      { text: "Широта", value: "longitude" },
      { text: "Долгота", value: "latitude" },
    ],
  }),

  methods: {
    clicl() {
      console.log(this.points);
    },
  },
};
</script>
