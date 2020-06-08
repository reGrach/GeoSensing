<template>
  <v-container fill-height fluid>
    <v-layout align-center justify-center>
      <v-flex xs12 sm8 md5>
        <v-card class="elevation-12">
          <v-card-title class="title">Вход в систему</v-card-title>
          <v-card-text>
            <v-form v-model="isValid">
              <v-text-field required :rules="rules.login" label="Логин" prepend-icon="mdi-account" v-model="login" />
              <v-text-field
                required
                :rules="rules.password"
                :type="showPassword ? 'text' : 'password'"
                label="Пароль"
                prepend-icon="mdi-lock"
                :append-icon="showPassword ? 'mdi-eye' : 'mdi-eye-off'"
                @click:append="showPassword = !showPassword"
                v-model="password"
              />
              <v-checkbox v-model="rememberMe" label="Запомнить меня"></v-checkbox>
            </v-form>
          </v-card-text>
          <v-alert text border="top" type="warning" v-model="showError">{{getError}}</v-alert>
          <v-divider></v-divider>
          <v-card-actions class="footerForm">
            <v-btn
              min-width="20%"
              color="success"
              @click.prevent="onSubmit"
              :disabled="!isValid"
              :loading="getProcessing"
            >Войти</v-btn>
            <v-spacer></v-spacer>
            <router-link to="/signup">
              <span>... или зарегистрироваться</span>
            </router-link>
          </v-card-actions>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import { SIGN_IN } from '@/store/actionsType'
import { mapGetters } from 'vuex'

export default {

  data: () => ({
    showPassword: false,
    login: '',
    password: '',
    rememberMe: false,
    isValid: false,
    rules: {
      login: [
        (v) => !!v || 'Необходимо ввести логин',
        (v) => (v && v.length >= 4) || 'Логин должен содержать минимум 4 символа'
      ],
      password: [
        (v) => !!v || 'Необходимо ввести пароль',
        (v) => (v && v.length >= 5) || 'Пароль должен содержать 5 и более символов'
      ]
    }
  }),

  computed: {
    ...mapGetters(['getError', 'getProcessing']),
    showError () {
      return this.getError != null
    }
  },

  methods: {

    onSubmit () {
      this.$store
        .dispatch(SIGN_IN, {
          login: this.login,
          password: this.password,
          rememberMe: this.rememberMe
        })
        .then(() => this.$router.push('/'))
    }
  }
}
</script>
<style scoped>
.title {
  color: #283593;
}

.footerForm {
  margin-left: 20px;
  margin-right: 20px;
}
</style>
