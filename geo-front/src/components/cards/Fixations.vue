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
                  <v-btn class="px-1 text-lowercase" tile text @click="show = !show">
                    <div style="font-size: 20px; letter-spacing: 0px">{{ showBtnTitle }}</div>
                  </v-btn>
                </v-card-title>
                <v-card-text>
                  <v-form ref="formAuto" :v-model="valid" id="demo">
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
                    <v-layout justify-center class="mt-5">
                      <v-btn
                        :class="'rounded-l-lg'"
                        style="font-size: 15px; letter-spacing: 0px"
                        tile
                        width="120px"
                        color="primary"
                        depressed
                        dark
                      >Отправить</v-btn>
                      <v-btn
                        style="font-size: 15px; letter-spacing: 0px"
                        tile
                        width="120px"
                        @click="resetAuto"
                        color="error"
                        depressed
                        dark
                      >Сбросить</v-btn>
                      <v-btn
                        :class="'rounded-r-lg'"
                        style="font-size: 15px; letter-spacing: 0px"
                        tile
                        width="120px"
                        @click="change"
                        :color="currentBtnColor"
                        depressed
                        dark
                      >{{ currentBtnTitle }}</v-btn>
                    </v-layout>
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
                  <v-form 
                  :v-model="valid"
                  ref="formManual">
                    <v-text-field
                    v-mask="'##.########'"
                    v-model="input.lat"
                    label="Широта" required />
                    <v-text-field
                    v-mask="'##.########'"
                    v-model="input.long"
                    label="Долгота" required />
                    <v-text-field 
                    v-mask="'###'"
                    v-model="input.height"
                    label="Высота" required />
                    <v-layout justify-center class="mt-5">
                      <v-btn
                        :class="'rounded-l-lg'"
                        style="font-size: 15px; letter-spacing: 0px"
                        tile
                        :disabled="valid"
                        width="130px"
                        color="primary"
                      >Отправить</v-btn>
                      <v-btn
                        :class="'rounded-r-lg'"
                        style="font-size: 15px; letter-spacing: 0px"
                        tile
                        @click="resetManual"
                        width="130px"
                        color="error"
                        depressed
                        dark
                      >Сбросить</v-btn>
                    </v-layout>
                  </v-form>
                </v-card-text>
              </v-card>
            </v-flex>
          </v-layout>
        </v-container>
      </v-tab-item>
      <v-tab-item>
        <v-card flat>
          <v-card-text>
            Когда Вы нажмёте "Начать",
            процесс сохранения координат Вашего устройства
            будет продолжаться до нажатия кнопки "Стоп".
          </v-card-text>
          <v-card-actions class="footerForm">
            <v-btn color="error" dark>Начать</v-btn>
          </v-card-actions>
        </v-card>
      </v-tab-item>
    </v-tabs-items>
  </v-card>
</template>

<script>
import Vue from 'vue'
import { VueMaskDirective } from 'v-mask'
Vue.directive('mask', VueMaskDirective);

export default {
  data: () => ({
    input: {
      lat: "",
      long: "",
      height: "",
    },
    valid: true,
    coords: "",
    hght: "",
    rules: {
    // Убрать регулярку, сделать маски
    coords: [
      (v) => !!v || "Нужно заполнить это поле",
      (v) => /.+\.+(?=\d)/.test(v) || "Координаты должны быть введены правильно",
    ],
    height: [
      (v) => !!v || "Нужно заполнить это поле",
      (v) => /(?=\d)/.test(v) || "Высота должна быть указана в метрах целым числом",
    ],
    },
    SHBtnStyle: {
      color: "cyan",
      fontSize: "22px",
      texttransform: "lowercase",
    },
    show: false,
    isNewPoint: true,
    tab: null,
    btn: {
      save: {
        color: "error",
        title: "Сохранить",
      },
      determine: {
        color: "success",
        title: "Определить",
      },
      show: {
        title: "простые",
      },
      hide: {
        title: "подробные",
      },
    },
  }),
  computed: {
    currentBtnColor() {
      return this.isNewPoint ? this.btn.determine.color : this.btn.save.color;
    },
    currentBtnTitle() {
      return this.isNewPoint ? this.btn.determine.title : this.btn.save.title;
    },
    showBtnTitle() {
      return this.show ? this.btn.hide.title : this.btn.show.title;
    },
  },
  //  Сделать единый метод отправки данных
  methods: {
    validate() {
      this.$refs.form.validate();
    },
    resetManual() {
      this.$refs.formManual.reset();
    },
    resetAuto() {
      this.$refs.formAuto.reset();
    },
    change() {
      this.isNewPoint = !this.isNewPoint;
    },
  },
};
</script>
