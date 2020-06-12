<template>
  <v-container fluid tag="section">
    <v-row justify="center">
      <v-col cols="12" md="4">
        <base-card
          class="v-card-profile"
          avatar="https://demos.creative-tim.com/vue-material-dashboard/img/marc.aba54d65.jpg"
        >
          <v-card-text class="text-center">
            <h6 class="display-1 mb-1 grey--text">РОЛЬ</h6>

            <h4 class="display-2 font-weight-light mb-3 black--text">German</h4>

            <v-btn color="success" rounded class="mr-0">Изменить аватар</v-btn>
          </v-card-text>
        </base-card>
      </v-col>
      <v-col cols="12" md="8">
        <base-material-card>
          <template v-slot:heading>
            <div class="display-2 font-weight-light">Edit Profile</div>

            <div class="subtitle-1 font-weight-light">Complete your profile</div>
          </template>

          <v-form>
            <v-container class="py-0">
              <v-row>
                <v-col cols="12" md="6">
                  <v-text-field label="Имя" class="purple-input" />
                </v-col>

                <v-col cols="12" md="6">
                  <v-text-field label="Фамилия" class="purple-input" />
                </v-col>

                <v-col cols="12">
                  <v-text-field label="Adress" class="purple-input" />
                </v-col>

                <v-col cols="12" md="4">
                  <v-text-field label="City" class="purple-input" />
                </v-col>

                <v-col cols="12" md="4">
                  <v-text-field label="Country" class="purple-input" />
                </v-col>

                <v-col cols="12" md="4">
                  <v-text-field class="purple-input" label="Postal Code" type="number" />
                </v-col>

                <v-col cols="12" class="text-right">
                  <v-btn color="success" class="mr-0">Обновить профиль</v-btn>
                </v-col>
              </v-row>
            </v-container>
          </v-form>
        </base-material-card>
      </v-col>


    </v-row>
  </v-container>
</template>

<script>
// import AvatarPicker from '@/components/AvatarPicker.vue';
import BaseCard from '@/components/BaseCard.vue';
import { mapGetters } from 'vuex';

export default {
  components: { BaseCard },
  data: () => ({
    form: {
      firstName: '',
      surName: '',
      avatar: '',
    },
    showAvatarPicker: false,
  }),

  computed: {
    ...mapGetters(['getError', 'getProcessing']),
    showError() {
      return this.getError != null;
    },
  },

  methods: {
    openAvatarPicker() {
      this.showAvatarPicker = true;
    },
    selectAvatar(avatar) {
      this.form.avatar = avatar;
    },
  },
};
</script>
