<template>
  <v-container fill-height fluid>
    <v-layout align-center justify-center>
      <v-flex xs12 sm8 md5>
        <v-card class="elevation-12">
          <v-card-title class="title">Регистрация</v-card-title>
          <v-card-text>
            <v-form v-model="isValid">
              <v-text-field
                required
                :rules="rules.login"
                label="Логин"
                prepend-icon="mdi-account"
                v-model="login"
              />
              <v-text-field
              ref="form"
                :rules="rules.password"
                :type="showPassword ? 'text' : 'password'"
                label="Пароль"
                prepend-icon="mdi-lock"
                :append-icon="showPassword ? 'mdi-eye' : 'mdi-eye-off'"
                @click:append="showPassword = !showPassword"
                v-model="password"
              />
              <v-text-field
                required
                type="password"
                :rules="confirmPasswordRules"
                label="Пароль еще раз"
                prepend-icon="mdi-redo"
                v-model="confirmPassword"
              />
            </v-form>
          </v-card-text>
          <v-alert text border="top" type="warning" v-model="showError">{{getError}}</v-alert>
          <v-divider></v-divider>
          <v-card-actions class="footerForm">
            <v-spacer></v-spacer>
            <v-btn
              min-width="20%"
              color="success"
              @click.prevent="onSubmit"
              :disabled="!isValid"
              :loading="getProcessing"
            >Зарегистрироваться</v-btn>
          </v-card-actions>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import { SIGN_UP } from '@/store/actionsType';
import { mapGetters } from 'vuex';

export default {
  data: () => ({
    showPassword: false,
    login: '',
    password: '',
    confirmPassword: '',
    isValid: false,
    rules: {
      login: [
        (v) => !!v || 'Необходимо ввести логин',
        (v) => (v && v.length >= 4) || 'Логин должен содержать минимум 4 символа',
      ],
      password: [
        (v) => !!v || 'Необходимо ввести пароль',
        (v) => (v && v.length >= 6) || 'Пароль должен содержать минимум 6 символов',
      ],
    },
  }),
  watch: {
    password: 'validateField',
    confirmPassword: 'validateField',
  },
  computed: {
    ...mapGetters(['getError', 'getProcessing']),
    showError() {
      return this.getError != null;
    },
    confirmPasswordRules() {
      const rules = [];

      const req = (v) => !!v || 'Необходимо ввести пароль еще раз';
      rules.push(req);

      if (this.password) {
        const rule = (v) => (!!v && v) === this.password || 'Пароли не совпадают';
        rules.push(rule);
      }

      return rules;
    },
  },

  methods: {
    onSubmit() {
      this.$store
        .dispatch(SIGN_UP, {
          login: this.login,
          password: this.password,
        })
        .then(() => this.$router.push('/signin'));
    },
    validateField() {
      this.$refs.form.validate();
    },
  },
};
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
