<template>
  <init-team v-if="isEasyUser"></init-team>
  <div v-else>В КОМАНДЕ</div>
</template>

<script>
import { mapGetters } from 'vuex';
import InitTeam from '../components/cards/InitTeam.vue';
import { GET_TEAM } from '../store/actionsType';


export default {
  components: { InitTeam },
  data: () => ({}),

  created() {
    if (!this.isEasyUser) {
      this.getMyTeam();
    }
  },

  computed: {
    ...mapGetters(['isEasyUser']),
  },

  methods: {
    getMyTeam() {
      this.$store.dispatch(GET_TEAM);
    },
  },
};
</script>
