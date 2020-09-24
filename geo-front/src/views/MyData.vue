<template>
  <v-container fill-height fluid>
    <v-sheet id="app">
      <yandex-map :coords="coords" :zoom="10" @click="onClick">
        <ymap-marker marker-id="123" :coords="coords1" :balloon-template="balloonTemplate" />
      </yandex-map>
    </v-sheet>
  </v-container>
</template>

<script>
import { yandexMap, ymapMarker } from 'vue-yandex-maps';

export default {
  components: { yandexMap, ymapMarker },

  data: () => ({
    coords: [60, 30],
    coords1: [0, 0],
  }),
  computed: {
    balloonTemplate() {
      return '<h1>Хей, не тыкай так сильно, больно вообще-то</h1><p>Я вот тебе координаты даю: ${this.coords1}, а ты мне?(((</p><img src="https://ru.meming.world/images/ru/thumb/7/73/%D0%A8%D0%B0%D0%B1%D0%BB%D0%BE%D0%BD_%D0%BA%D0%BE%D1%82.jpg/300px-%D0%A8%D0%B0%D0%B1%D0%BB%D0%BE%D0%BD_%D0%BA%D0%BE%D1%82.jpg">';
    },
  },
  methods: {
    onClick(e) {
      this.coords1 = e.get('coords');
    },
  },
};
</script>

<style scoped>
#app {
  width: 100%;
  height: 94vh;
}

.ymap-container {
  height: 94%;
}
</style>
