import Vue from 'vue'
import YmapPlugin from 'vue-yandex-maps'

const settings = {
    apiKey: '989c3b6b-bc85-4d4e-9c43-9ad29db3c71e',
    lang: 'ru_RU',
    coordorder: 'latlong',
    version: '2.1'
  }

Vue.use(YmapPlugin, settings);