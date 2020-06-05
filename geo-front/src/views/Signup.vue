<template>
  <v-container fill-height fluid>
    <v-layout align-center justify-center>
      <v-flex xs12 sm8 md5>
        <v-card class="elevation-12">
          <v-card-title class="title">Регистрация</v-card-title>
          <v-card-text>
            <v-form>
              <v-text-field required label="Логин" prepend-icon="mdi-account" v-model="login" />
              <v-text-field
                :type="showPassword ? 'text' : 'password'"
                label="Пароль"
                prepend-icon="mdi-lock"
                :append-icon="showPassword ? 'mdi-eye' : 'mdi-eye-off'"
                @click:append="showPassword = !showPassword"
                v-model="password"
              />
            </v-form>
          </v-card-text>
          <v-alert text border="top" type="warning" v-model="error">{{error}}</v-alert>
          <v-divider></v-divider>
          <v-card-actions class="footerForm">
            <v-spacer></v-spacer>
            <v-btn
              min-width="15%"
              color="info"
              @click.prevent="onSubmit"
              :loading="processing"
            >Зарегистрироваться</v-btn>
          </v-card-actions>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import { SIGN_UP } from '@/store/actionsType'
export default {
  data: () => ({
    showPassword: false,
    login: '',
    password: ''
  }),
  computed: {
    error () {
      return this.$store.getters.getError
    },
    processing () {
      return this.$store.getters.getProcessing
    }
  },
  methods: {
    onSubmit () {
      this.$store
        .dispatch(SIGN_UP, {
          login: this.login,
          password: this.password
        })
        .then(() => this.$router.push('/signin'))
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
