import Vue from 'vue'
import Vuetify from 'vuetify/lib'
import ru from 'vuetify/es5/locale/ru'
import MainIcon from '@/components/icons/MainIcon.vue'

Vue.use(Vuetify)

export default new Vuetify({
  icons: {
    values: {
      geo: { // name of our custom icon
        component: MainIcon
      }
    }
  },
  lang: {
    locales: { ru },
    current: 'ru'
  }
})
