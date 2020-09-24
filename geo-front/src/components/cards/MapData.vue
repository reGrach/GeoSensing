<template>
      <yandex-map style="width: 100%; height: 100%;" :coords="coords" :zoom="10">
        <ymap-marker v-for="point in points" :key="point.number"  :markerId="point.number" :coords = "point.coords" />
      </yandex-map>
</template>

<script>
import { yandexMap, ymapMarker } from 'vue-yandex-maps';

export default {
  components: { yandexMap, ymapMarker },
  props: {
      points: Array
  },
  data: () => ({
    coords: [60, 30]
  }),
};
</script>
