<template>
  <v-container fill-height fluid>
    <v-layout align-center justify-center>
      <v-flex xs12 sm8 md5>
        <v-card class="elevation-12">
          <v-card-title class="title">Вход в систему</v-card-title>
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
          <v-alert text border="top" type="warning" v-model="showError">{{error}}</v-alert>
          <v-divider></v-divider>
          <v-card-actions class="footerForm">
            <router-link to="/signup">
              <span>Зарегистрируйтесь, если у Вас еще нет профиля!</span>
            </router-link>
            <v-spacer></v-spacer>
            <v-btn min-width="15%" color="info" @click.prevent="onSubmit" :loading="processing">Войти</v-btn>
          </v-card-actions>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import { SIGN_IN } from '@/store/actionsType'
export default {
  data: () => ({
    showPassword: false,
    login: '',
    password: ''
  }),
  computed: {
    showError () {
      return this.$store.getters.getError != null
    },
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
        .dispatch(SIGN_IN, {
          login: this.login,
          password: this.password
        })
        .then(() => this.$router.push({ name: 'main' }))
    },

    test () {
      console.log('TEST')
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
