<template>
  <v-container fill-height fluid>
    <v-layout align-center justify-center>
      <v-flex xs12 sm8 md6>
        <v-card class="elevation-0">
          <v-tabs v-model="tab" grow color="indigo">
            <v-tab>Новая команда</v-tab>
            <v-tab>Стать участником</v-tab>
          </v-tabs>

          <v-tabs-items v-model="tab">
            <v-tab-item>
              <v-card height="120" flat>
                <v-card-text>
                  <v-form v-model="createForm.isValid">
                    <v-text-field
                      v-model="createForm.title"
                      :rules="createForm.rules"
                      required
                      label="Название"
                      prepend-icon="mdi-pencil"
                    />
                    <v-text-field
                      v-model="createForm.color"
                      label="Цвет"
                      prepend-icon="mdi-palette"
                      return-masked-value
                      :mask="mask"
                      hide-details
                      solo
                    >
                      <template v-slot:append>
                        <v-menu v-model="menu" offset-y>
                          <template v-slot:activator="{ on }">
                            <div :style="swatchStyle" v-on="on" />
                          </template>
                          <v-card>
                            <v-card-text class="pa-0">
                              <v-color-picker
                                v-model="createForm.color"
                                flat
                                show-swatches
                                hide-inputs
                                hide-canvas
                                disabled
                              />
                            </v-card-text>
                          </v-card>
                        </v-menu>
                      </template>
                    </v-text-field>
                  </v-form>
                </v-card-text>
                <v-divider></v-divider>
                <v-card-actions class="footerForm">
                  <v-btn
                    color="success"
                    @click.prevent="CreateTeam"
                    :disabled="!createForm.isValid"
                    :loading="getProcessing"
                    block
                  >Создать</v-btn>
                </v-card-actions>
              </v-card>
            </v-tab-item>
            <v-tab-item>
              <v-card height="120" flat>
                <v-card-text>
                  <v-form v-model="isValidChoice">
                    <v-select
                      v-model="selectedTeam"
                      :items="teams"
                      item-value="id"
                      label="Выберите команду"
                      no-data-text="Команды отсутствуют"
                      required
                      :rules="[v => !!v || 'Обязательное поле']"
                    >
                      <template v-slot:item="data">
                        <v-chip
                          style="cursor: pointer;"
                          :color="data.item.color"
                          label
                          v-text="data.item.title"
                        ></v-chip>
                      </template>
                      <template v-slot:selection="data">
                        <v-chip
                          style="cursor: pointer;"
                          :color="data.item.color"
                          label
                          v-text="data.item.title"
                        ></v-chip>
                      </template>
                    </v-select>
                  </v-form>
                </v-card-text>
                <v-divider></v-divider>
                <v-card-actions class="footerForm">
                  <v-btn
                    color="success"
                    @click.prevent="JoinTeam"
                    :disabled="!isValidChoice"
                    :loading="getProcessing"
                    block
                  >Присоединиться</v-btn>
                </v-card-actions>
              </v-card>
            </v-tab-item>
          </v-tabs-items>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import { mapGetters } from 'vuex';
import { GET_ALL_TEAMS, CREATE_TEAM, JOIN_TEAM } from '@/store/actionsType';

export default {
  data: () => ({
    tab: null,
    createForm: {
      isValid: false,
      title: null,
      color: '#1976D2FF',
      rules: [(v) => !!v || 'Необходимо ввести название'],
    },
    isValidChoice: false,
    teams: [],
    selectedTeam: null,
    mask: '!#XXXXXXXX',
    menu: false,
  }),

  created() {
    this.$store.dispatch(GET_ALL_TEAMS).then((data) => {
      this.teams = data;
    });
  },

  computed: {
    ...mapGetters(['isEasyUser', 'getProcessing']),
    swatchStyle() {
      return {
        backgroundColor: this.createForm.color,
        cursor: 'pointer',
        height: '30px',
        width: '50px',
        borderRadius: '4px',
      };
    },
  },
  methods: {
    CreateTeam() {
      const { title, color } = this.createForm;
      this.$store.dispatch(CREATE_TEAM, { title, color });
    },
    JoinTeam() {
      this.$store.dispatch(JOIN_TEAM, this.selectedTeam);
    },
  },
};
</script>
