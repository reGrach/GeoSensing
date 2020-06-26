<template>
  <v-container fill-height fluid>
    <v-row justify-center>
      <v-col cols="12" md="4">
        <v-card>
          <v-card-text class="text-center">
            <v-avatar size="192" class="mx-auto v-card-avatar elevation-6" color="grey">
<<<<<<< HEAD
              <v-img :src="getAvatar" alt="Avatar" @click="openAvatarLoader"/>
=======
              <v-img :src="avatar" alt="Avatar" @click="openAvatarLoader">
                <template v-slot:placeholder>
                  <v-img :src="placeholder"></v-img>
                </template>
              </v-img>
>>>>>>> develop
            </v-avatar>
            <v-list-item>
              <v-list-item-content>
                <v-list-item-title class="title">{{currentLogin}}</v-list-item-title>
                <v-list-item-subtitle>{{getRoleName}}</v-list-item-subtitle>
              </v-list-item-content>
            </v-list-item>
          </v-card-text>
        </v-card>
      </v-col>
      <v-col cols="12" md="8">
        <v-form>
          <v-container class="py-0">
            <v-row>
              <v-col cols="12" md="6">
                <v-text-field label="Имя" class="purple-input" v-model="form.name" />
              </v-col>

              <v-col cols="12" md="6">
                <v-text-field label="Фамилия" class="purple-input" v-model="form.surName" />
              </v-col>
            </v-row>
            <v-row>
              <v-spacer></v-spacer>
              <v-btn color="success" class="mr-0" @click="onSubmit">Обновить профиль</v-btn>
            </v-row>
          </v-container>
        </v-form>
      </v-col>
    </v-row>
<<<<<<< HEAD
    <avatar-loader
      v-model="showAvatarLoader"
      @uploaded="init"
    ></avatar-loader>
=======
    <avatar-loader v-model="showAvatarLoader" @uploaded="init"></avatar-loader>
>>>>>>> develop
  </v-container>
</template>

<script>
import AvatarLoader from '@/components/modals/AvatarLoader.vue';
import { GET_PROFILE, UPDATE_PROFILE } from '@/store/actionsType';
import { mapGetters } from 'vuex';
import placeholder from '../assets/placeholder.png';

export default {
  components: { AvatarLoader },
  data: () => ({
    form: {
      name: '',
      surName: '',
    },
    avatar: null,
    showAvatarLoader: false,
<<<<<<< HEAD
=======
    placeholder,
>>>>>>> develop
  }),

  created() {
    this.init();
  },

  computed: {
    ...mapGetters(['getError', 'getProcessing', 'getRoleName', 'currentLogin']),
<<<<<<< HEAD
    getAvatar() {
      if (this.avatar) {
        return this.avatar;
      }
      return placeholder;
    },
=======
>>>>>>> develop
    showError() {
      return this.getError != null;
    },
  },

  methods: {
    openAvatarLoader() {
      this.showAvatarLoader = true;
    },
    init() {
      this.$store.dispatch(GET_PROFILE).then((data) => {
        this.form.name = data.name;
        this.form.surName = data.surName;
        this.avatar = data.avatarSrc;
      });
    },
    onSubmit() {
      this.$store.dispatch(UPDATE_PROFILE, {
        name: this.form.name,
        surName: this.form.surName,
      });
    },
  },
};
</script>
<style lang="scss">
.v-card-avatar {
  position: relative;
  top: -64px;
  margin-bottom: -48px;
}
</style>
