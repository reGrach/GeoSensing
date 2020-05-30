<template>
  <v-container class="fill-height" fluid>
    <v-row align="center" justify="center">
      <v-col cols="12" sm="8" md="5">
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
          <v-divider></v-divider>
          <v-card-actions>
            <router-link to="/signup">
              <span>Вы еще не с нами?</span>
            </router-link>
            <v-spacer></v-spacer>
            <v-btn color="info" @click.prevent="test">Войти</v-btn>
          </v-card-actions>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import TestApi from '../api/test'

export default {
  data: () => ({
    showPassword: false,
    login: '',
    password: ''
  }),
  methods: {
    signin () {
      this.$store.dispatch('SIGNIN', {
        login: this.login,
        password: this.password
      })
    },

    test () {
      TestApi.default()
        .then((resp) => console.log(resp.data))
        .catch((error) => console.log(error))
    }
  }
}
</script>
<style scoped>
.title{
  color: #283593;
}
</style>
