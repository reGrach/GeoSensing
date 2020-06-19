<template>
<v-card.rounded-sm>
  <v-tabs flat v-model="tab" fixed-tabs background-color="indigo" dark>
    <v-tab v-for="item in items" :key="item.tab">{{ item.tab }}</v-tab>
  </v-tabs>
  <v-tabs-items v-model="tab">
    <v-tab-item v-for="item in items" :key="item.tab">
      <v-card flat>
        <v-card-text>{{ item.content }}</v-card-text>
      </v-card>
    </v-tab-item>
  </v-tabs-items>
</v-card.rounded-sm>
</template>

<script>
export default {
  data() {
    return {
      tab: null,
      items: [
        { tab: "Авто", content: "Auto" },
        { tab: "Ручной", content: "Manual" },
        { tab: "Слежка", content: "Watch" }
      ]
    };
  }
};
</script>
