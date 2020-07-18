<template>
  <v-card tile class="elevation-0">
    <v-tabs v-model="tab" color="indigo" flat grow>
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
                <v-card-title class="title">
                  Координаты:
                  <v-btn class="pl-1 pr-1 text-lowercase" tile text @click="show = !show">
                    <div style="font-size: 20px; letter-spacing: 0px">{{ showBtnTitle }}</div>
                  </v-btn>
                  <v-btn
                    absolute
                    right
                    width="150px"
                    @click="change"
                    :color="currentBtnColor"
                    depressed
                    dark
                  >{{ currentBtnTitle }}</v-btn>
                </v-card-title>
                <v-card-text>
                  <v-form id="demo">
                    <v-text-field disabled label="Широта" />
                    <v-text-field disabled label="Долгота" />
                    <v-text-field disabled label="Высота" />
                    <transition name="fade">
                      <!-- Костыль! надо переделать -->
                      <v-layout>
                        <v-flex>
                          <v-text-field v-if="show" disabled label="Направление" />
                          <v-text-field v-if="show" disabled label="Скорость" />
                          <v-text-field v-if="show" disabled label="Точность" />
                        </v-flex>
                      </v-layout>
                      <!-- Конец костыля. Тут нужен транзишн груп -->
                    </transition>
                  </v-form>
                </v-card-text>
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
    SHBtnStyle: {
      color: 'cyan',
      fontSize: '22px',
      texttransform: 'lowercase',
    },
    show: false,
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
    btnShow: {
      title: 'простые',
    },
    btnHide: {
      title: 'подробные',
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
    showBtnTitle() {
      if (this.show) {
        return this.btnHide.title;
      }
      return this.btnShow.title;
    },
  },
  methods: {
    change() {
      this.isNewPoint = !this.isNewPoint;
    },
  },
};
</script>
