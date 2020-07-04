<template>
  <v-card class="v-card--material pa-3">
    <div class="d-flex grow">
      <div class="ml-2">
        <div class="ml-auto text-left">
          <div class="grey--text font-weight-light" v-text="title" />
          <span class="display-1 font-weight-light">{{ value }}</span>
        </div>
      </div>
      <v-sheet elevation="6" class="pa-4" dark :color="color" rounded>
        <v-icon large v-text="icon" />
      </v-sheet>
    </div>
  </v-card>
</template>

<script>
export default {
  props: {
    color: {
      type: String,
      default: 'success',
    },
    icon: {
      type: String,
      required: true,
    },
    title: {
      type: String,
      default: undefined,
    },
    value: {
      type: String,
      default: undefined,
    },
  },
};
</script>

<style lang="sass">
.v-card--material
  display: flex
  flex-wrap: wrap
  position: relative

  > div:first-child
    justify-content: space-between
</style>
