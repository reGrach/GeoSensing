<template>
  <v-snackbar
    v-model="look"
    :timeout="2000"
    absolute
    top
    right
    :color="nColor"
  >
    {{nMsg}}
  </v-snackbar>
</template>

<script>
import { mapGetters } from 'vuex';

export default {
  data: () => ({
    look: false,
  }),
  watch: {
    nShow() {
      this.look = true;
    },
  },
  computed: {
    ...mapGetters(['nMsg', 'nColor', 'nShow']),
  },
};
</script>
