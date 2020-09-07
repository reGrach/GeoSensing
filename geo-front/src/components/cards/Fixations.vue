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
                    <v-text-field disabled label="Широта" :value="location.coords.latitude"/>
                    <v-text-field disabled label="Долгота" :value="location.coords.longitude"/>
                    <v-text-field disabled label="Высота" :value="location.coords.altitude"/>
                    <transition name="fade">
                      <v-layout>
                        <v-flex>
                          <v-text-field v-if="show" disabled label="Точность высоты" :value="location.coords.altitudeAccuracy"/>
                          <v-text-field v-if="show" disabled label="Точность координат" :value="location.coords.accuracy"/>
                          <v-text-field v-if="show" disabled label="Направление" :value="location.coords.heading"/>
                          <v-text-field v-if="show" disabled label="Скорость " :value="location.coords.speed"/>
                        </v-flex>
                      </v-layout>
                    </transition>
                    <v-layout justify-center class="mt-5">
                      <v-btn
                        :class="'rounded-l-lg'"
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
                        @click="locateMe"
                        :color="currentBtnColor"
                        depressed
                        dark
                      >{{ currentBtnTitle }}</v-btn>
                      <!-- для дебага, ес чо -->
                      <!-- <div v-if="gettingLocation">
                        <i>Getting your location...</i>
                      </div>
                      <div v-if="errorStr">
                        Sorry, but the following error
                        occurred: {{errorStr}}
                      </div> -->
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
                  <v-form :v-model="valid" ref="formManual">
                    <v-text-field
                      :rules="rules.fillCoords"
                      v-mask="'##.########'"
                      v-model="input.lat"
                      label="Широта"
                      required
                    />
                    <v-text-field
                      :rules="rules.fillCoords"
                      v-mask="'##.########'"
                      v-model="input.long"
                      label="Долгота"
                      required
                    />
                    <v-text-field
                      :rules="rules.fillHeight"
                      v-mask="'###'"
                      v-model="input.height"
                      label="Высота"
                      required
                    />
                    <v-layout justify-center class="mt-5">
                      <v-btn
                        :class="'rounded-l-lg'"
                        style="font-size: 15px; letter-spacing: 0px"
                        tile
                        @click="resetManual"
                        width="130px"
                        color="error"
                        depressed
                        dark
                      >Сбросить</v-btn>
                      <v-btn
                        :class="'rounded-r-lg'"
                        style="font-size: 15px; letter-spacing: 0px"
                        tile
                        :disabled="valid"
                        width="130px"
                        color="primary"
                      >Отправить</v-btn>
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
            <v-layout justify-center class="mt-5">
              <v-btn
                :class="'rounded-l-lg'"
                style="font-size: 15px; letter-spacing: 0px"
                tile
                width="120px"
                color="success"
                depressed
                dark
              >Начать</v-btn>
              <v-btn
                :class="'rounded-r-lg'"
                style="font-size: 15px; letter-spacing: 0px"
                tile
                width="120px"
                color="error"
                depressed
                dark
              >Стоп</v-btn>
            </v-layout>
          </v-card-actions>
        </v-card>
      </v-tab-item>
    </v-tabs-items>
  </v-card>
</template>

<script>
import Vue from "vue";
import { VueMaskDirective } from "v-mask";
Vue.directive("mask", VueMaskDirective);
export default {
  data: () => ({
    location: {
      coords: {
        latitude: "",
        longitude: "",
        altitude: "",
        altitudeAccuracy: "",
        accuracy: "",
        speed: "",
        heading: "",
      },
    },
    errorStr: "",
    gettingLocation: false,
    input: {
      lat: "",
      long: "",
      altitude: "",
    },
    rules: {
      fillCoords: [
        (v) => !!v || "Нужно заполнить это поле",
        (v) => (v && v.length > 8) || "Поле должно быть заполнено полностью",
      ],
      fillHeight: [(v) => !!v || "Нужно заполнить это поле"],
    },

    valid: true,
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
        color: "primary",
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
    async getLocation() {
      return new Promise((resolve, reject) => {
        if (!("geolocation" in navigator)) {
          reject(new Error("Geolocation is not available."));
        }

        navigator.geolocation.getCurrentPosition(
          (pos) => {
            resolve(pos);
          },
          (err) => {
            reject(err);
          }
        );
      });
    },
    async locateMe() {
      this.gettingLocation = true;
      try {
        this.gettingLocation = false;
        this.location = await this.getLocation();
      } catch (e) {
        this.gettingLocation = false;
        this.errorStr = e.message;
      }
    },
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
