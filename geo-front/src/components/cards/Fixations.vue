<template>
  <v-card tile class="elevation-0">
    <v-tabs v-model="tab" flat fixed-tabs background-color="indigo" dark>
      <v-tab>Авто</v-tab>
      <v-tab>Ручной</v-tab>
      <v-tab>Слежение</v-tab>
    </v-tabs>
    <v-tabs-items v-model="tab">
      <v-tab-item>
        <v-container fill-height fluid>
          <v-layout align-center justify-center>
            <v-flex xs12 sm8 md6>
              <v-card class="elevation-0">
                <v-card-title class="title">Координаты</v-card-title>
                <v-card-text>
                  <v-form>
                    <v-text-field disabled label="Широта" />
                    <v-text-field disabled label="Долгота" />
                    <v-text-field disabled label="Высота" />
                  </v-form>
                </v-card-text>
                <v-layout align-center justify-center>
                  <v-btn class="mr-5" v-text="currentBtnTitle" @click="change" :color="currentBtnColor" dark></v-btn>
                </v-layout>
              </v-card>
            </v-flex>
          </v-layout>
        </v-container>
      </v-tab-item>
      <v-tab-item>
        <v-container fill-height fluid>
          <v-layout align-center justify-center>
            <v-flex xs12 sm8 md6>
              <v-card class="elevation-0">
                <v-card-title class="title">Ввод</v-card-title>
                <v-card-text>
                  <v-form>
                    <v-text-field label="Широта" />
                    <v-text-field label="Долгота" />
                    <v-text-field label="Высота" />
                  </v-form>
                </v-card-text>
              </v-card>
            </v-flex>
          </v-layout>
        </v-container>
      </v-tab-item>
      <v-tab-item>
        <v-card flat>
          <v-card-text>Когда Вы нажмёте "Начать", процесс сохранения координат Вашего устройства будет продолжаться до нажатия кнопки "Стоп".</v-card-text>
          <v-card-actions class="footerForm">
            <v-btn color="#ff3333" dark>Начать</v-btn>
          </v-card-actions>
        </v-card>
      </v-tab-item>
    </v-tabs-items>
  </v-card>
</template>

<script>
export default {
  data: () => ({
    isNewPoint: true,
    tab: null,
    btnSave: {
      color: '#ff3333',
      title: 'Сохранить',
    },
    btnDetermine: {
      color: '#29cc29',
      title: 'Определить',
    },
  }),
  computed: {
    currentBtnColor() {
      if (this.isNewPoint) {
        return this.btnDetermine.color;
      }
      return this.btnSave.color;
    },
    currentBtnTitle() {
      if (this.isNewPoint) {
        return this.btnDetermine.title;
      }
      return this.btnSave.title;
    },
  },
  methods: {
    change() {
      this.isNewPoint = !this.isNewPoint;
    },
  },
};
</script>
