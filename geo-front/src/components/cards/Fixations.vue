<template>
  <v-card tile class="elevation-0">
    <v-tabs v-model="tab" color="indigo" flat grow>
      <v-tab v-for="tab in modes" :key="tab.key">{{ tab.name }}</v-tab>
    </v-tabs>
    <v-tabs-items v-model="tab">
      <v-tab-item>
        <v-container fill-height fluid>
          <v-layout align-center justify-center>
            <v-flex xs12 sm8 md6>
              <v-card class="elevation-0">
                <v-card-title class="title">Ввод</v-card-title>
                <v-card-text>
                  <v-form v-model="validManual" ref="formManual">
                    <v-text-field
                      :rules="rules.fillCoords"
                      v-model="formPoint.manual.latitude"
                      label="Широта"
                      required
                    />
                    <v-text-field
                      :rules="rules.fillCoords"
                      v-model="formPoint.manual.longitude"
                      label="Долгота"
                      required
                    />
                    <v-text-field
                      :rules="rules.fillHeight"
                      v-model="formPoint.manual.altitude"
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
                        >Сбросить</v-btn
                      >
                      <v-btn
                        :class="'rounded-r-lg'"
                        style="font-size: 15px; letter-spacing: 0px"
                        tile
                        @click="submit"
                        :disabled="!validManual"
                        width="130px"
                        color="primary"
                        >Отправить</v-btn
                      >
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
                <v-card-title class="title">
                  Координаты:
                  <v-btn
                    class="px-1 text-lowercase"
                    tile
                    text
                    @click="show = !show"
                  >
                    <div style="font-size: 20px; letter-spacing: 0px">
                      {{ isExtension }}
                    </div>
                  </v-btn>
                </v-card-title>
                <v-card-text>
                  <v-form ref="formAuto" :v-model="valid">
                    <v-text-field
                      disabled
                      label="Широта"
                      v-model="formPoint.auto.latitude"
                    />
                    <v-text-field
                      disabled
                      label="Долгота"
                      v-model="formPoint.auto.longitude"
                    />
                    <v-text-field
                      disabled
                      label="Высота"
                      v-model="formPoint.auto.altitude"
                    />
                    <transition name="fade">
                      <v-layout>
                        <v-flex>
                          <v-text-field
                            v-if="show"
                            disabled
                            label="Точность высоты"
                            v-model="formPoint.auto.altitudeAccuracy"
                          />
                          <v-text-field
                            v-if="show"
                            disabled
                            label="Точность координат"
                            v-model="formPoint.auto.accuracy"
                          />
                          <v-text-field
                            v-if="show"
                            disabled
                            label="Направление"
                            v-model="formPoint.auto.heading"
                          />
                          <v-text-field
                            v-if="show"
                            disabled
                            label="Скорость "
                            v-model="formPoint.auto.speed"
                          />
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
                        >Сбросить</v-btn
                      >
                      <v-btn
                        :class="'rounded-r-lg'"
                        style="font-size: 15px; letter-spacing: 0px"
                        tile
                        width="120px"
                        @click="locateMe"
                        :color="currentBtnColor"
                        depressed
                        dark
                        >{{ currentBtnTitle }}</v-btn
                      >
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
            Когда Вы нажмёте "Начать", процесс сохранения координат Вашего
            устройства будет продолжаться до нажатия кнопки "Стоп".
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
                >Начать</v-btn
              >
              <v-btn
                :class="'rounded-r-lg'"
                style="font-size: 15px; letter-spacing: 0px"
                tile
                width="120px"
                color="error"
                depressed
                dark
                >Стоп</v-btn
              >
            </v-layout>
          </v-card-actions>
        </v-card>
      </v-tab-item>
    </v-tabs-items>
  </v-card>
</template>

<script>
import modes from '@/common/modePoint';
import { POINT } from '../../store/actionsType';

export default {
  props: {
    cExpId: Number,
  },
  data: () => ({
    modes,
    tab: null,
    formPoint: {
      auto: {
        accuracy: null,
        altitude: null,
        altitudeAccuracy: null,
        heading: null,
        latitude: null,
        longitude: null,
        speed: null,
      },
      manual: {
        altitude: null,
        latitude: null,
        longitude: null,
      },
    },
    errorStr: '',
    isDefinedLocation: false,
    gettingLocation: false,
    rules: {
      fillCoords: [
        (v) => !!v || 'Обязательное поле',
        (v) => {
          const pattern = /^(-?\d{1,3}([.|,]\d{1,8})?)$/;
          return pattern.test(v) || 'Неверный формат ввода координат';
        },
      ],
      fillHeight: [
        (v) => !!v || 'Обязательное поле',
        (v) => {
          const pattern = /^\d{1,3}$/;
          return pattern.test(v) || 'Неверный формат ввода координат';
        },
      ],
    },

    valid: true,
    validManual: false,
    SHBtnStyle: {
      color: 'cyan',
      fontSize: '22px',
      texttransform: 'lowercase',
    },
    show: false,
    isNewPoint: true,
  }),
  computed: {
    currentBtnColor() {
      return this.isDefinedLocation && this.formPoint.auto.altitude ? 'success' : 'primary';
    },
    currentBtnTitle() {
      return this.isDefinedLocation && this.formPoint.auto.altitude ? 'Отправить' : 'Определить';
    },
    isExtension() {
      return this.show ? 'простые' : 'подробные';
    },
    getCurrentMode() {
      return this.modes[this.tab].key;
    },
  },
  //  Сделать единый метод отправки данных
  methods: {
    async getLocation() {
      return new Promise((resolve, reject) => {
        if (!('geolocation' in navigator)) {
          reject(new Error('Geolocation is not available.'));
        }

        navigator.geolocation.getCurrentPosition(
          (pos) => {
            resolve(pos);
          },
          (err) => {
            reject(err);
          },
        );
      });
    },
    async locateMe() {
      if (this.isDefinedLocation) {
        this.submit();
        this.isDefinedLocation = false;
      } else {
        try {
          const { coords } = await this.getLocation();
          this.formPoint.auto = this.parseLocation(coords);
          this.isDefinedLocation = true;
        } catch (e) {
          this.errorStr = e.message;
        }
      }
    },
    parseLocation(coords) {
      return {
        accuracy: coords.accuracy,
        altitude: coords.altitude,
        altitudeAccuracy: coords.altitudeAccuracy,
        heading: coords.heading,
        latitude: coords.latitude,
        longitude: coords.longitude,
        speed: coords.speed,
      };
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
    submit() {
      const point = {
        ...this.formPoint[this.getCurrentMode],
        mode: this.getCurrentMode,
        experimentId: this.cExpId,
      };
      console.log(point);
      this.$store.dispatch(POINT, point)
        .then(() => {
          if (this.getCurrentMode === 'auto') {
            this.resetAuto();
          }
          if (this.getCurrentMode === 'manual') {
            this.resetManual();
          }
        })
        .catch(() => {});
    },
  },
};
</script>
