<template>
  <v-container style="padding: unset" fluid>
    <yandex-map
      ref="ymap"
      :style="sizes"
      :coords="coords"
      :zoom="10"
      :controls="['zoomControl', 'typeSelector', 'fullscreenControl']"
    >
      <ymap-marker
        v-for="point in points"
        :key="point.number"
        :markerId="point.number"
        :coords="point.coords"
      />
    </yandex-map>
  </v-container>
</template>

<script>
import { yandexMap, ymapMarker } from 'vue-yandex-maps';

export default {
  components: { yandexMap, ymapMarker },
  props: {
    points: Array,
  },
  data: () => ({
    coords: [60, 30],
    positionMap: {
      left: 0,
      top: 0,
    },
  }),
  computed: {
    sizes() {
      const { width, height } = this.$vuetify.breakpoint;
      if (this.$el) {
        this.updatePosition();
      }
      return {
        width: `${width - this.positionMap.left}px`,
        height: `${height - this.positionMap.top - 36}px`,
      };
    },
  },
  mounted() {
    this.updatePosition();
    this.$forceUpdate();
  },
  methods: {
    updatePosition() {
      const pos = this.$el.getBoundingClientRect();
      this.positionMap.left = pos.left;
      this.positionMap.top = pos.top;
    },
  },
};
</script>
