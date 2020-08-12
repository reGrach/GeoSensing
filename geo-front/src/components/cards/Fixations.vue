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
                </v-card-title>
                <v-card-text>
                  <v-form ref="form" :v-model="valid" id="demo">
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
                    <v-layout justify-center>
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
                      @click="reset"
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
                  <v-form :v-model="valid" ref="form">
                    <v-text-field :rules="coordsRules" label="Широта" required />
                    <v-text-field :rules="coordsRules" label="Долгота" required />
                    <v-text-field :rules="hghtRules" label="Высота" required />
                    <v-layout justify-center >
                    <v-btn 
                    :class="'rounded-l-lg'"
                      style="font-size: 15px; letter-spacing: 0px"
                      tile
                    :disabled="valid" width="130px" color="primary">Отправить</v-btn>
                    <v-btn
                    :class="'rounded-r-lg'"
                      style="font-size: 15px; letter-spacing: 0px"
                      tile
                      @click="reset"
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
    valid: true,
    coords: "",
    coordsRules: [
      (v) => !!v || "Нужно заполнить это поле",
      (v) =>
        /.+\.+(?=\d)/.test(v) || "Координаты должны быть введены правильно",
    ],
    hght: "",
    hghtRules: [
      (v) => !!v || "Нужно заполнить это поле",
      (v) =>
        /(?=\d)/.test(v) || "Высота должна быть указана в метрах целым числом",
    ],
    SHBtnStyle: {
      color: "cyan",
      fontSize: "22px",
      texttransform: "lowercase",
    },
    show: false,
    isNewPoint: true,
    tab: null,
    btnSave: {
      color: "error",
      title: "Сохранить",
    },
    btnDetermine: {
      color: "#29cc29",
      title: "Определить",
    },
    btnShow: {
      title: "простые",
    },
    btnHide: {
      title: "подробные",
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
  //  Сделать единый метод отправки данных
  methods: {
    validate() {
      this.$refs.form.validate();
    },
    reset() {
      this.$refs.form.reset();
    },
    change() {
      this.isNewPoint = !this.isNewPoint;
    },
  },
};
</script>
