<template>
  <init-team v-if="isEasyUser"></init-team>
  <div v-else>В КОМАНДЕ</div>
</template>

<script>
import { mapGetters } from 'vuex';
import InitTeam from '../components/cards/InitTeam.vue';

export default {
  components: { InitTeam },
  data: () => ({}),

  computed: {
    ...mapGetters(['isEasyUser']),
  },

  methods: {},
};
</script>
