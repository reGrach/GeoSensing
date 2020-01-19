<template>
  <v-content>
    <v-container fluid fill-height>
      <v-layout align-left justify-left>
          <!-- <v-card class="elevation-12">
            <v-toolbar color="primary" dark flat>
              <v-toolbar-title>Получение позиции</v-toolbar-title>
              <v-spacer></v-spacer>
              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn @click="geo" color="blue">GetCurPos</v-btn>
              </v-card-actions>
            </v-toolbar>
            <v-card-text>
              <v-switch v-model="switch1" :label="`Mode: ${switch1.toString()}`"></v-switch>
              <v-form>
                <v-text-field id="lat" label="lattitude" name="lat" type="text">{{lat}}</v-text-field>
                <v-text-field id="long" label="longitude" name="long" type="text">{{long}}</v-text-field>
              </v-form>
            </v-card-text>
          </v-card> -->

          <v-card align='left' height="100%" width="200" class="mx-auto">
            <v-navigation-drawer>
              <v-list-item>
                <v-list-item-content>
                  <v-list-item-title class="title">GeoSensing</v-list-item-title>
                  <v-list-item-subtitle>make maps sipmplier</v-list-item-subtitle>
                </v-list-item-content>
              </v-list-item>
              <v-divider></v-divider>
              <v-list dense nav>
                <v-list-item v-for="item in items" :key="item.title" link>
                  <v-list-item-icon>
                    <v-icon>{{ item.icon }}</v-icon>
                  </v-list-item-icon>
                  <v-list-item-content>
                    <v-list-item-title>{{ item.title }}</v-list-item-title>
                  </v-list-item-content>
                </v-list-item>
              </v-list>
            </v-navigation-drawer>
          </v-card>
      </v-layout>
    </v-container>
  </v-content>
</template>

<script>
export default {
  name: "Geo",
  data() {
    return {
      namebutton: "get current position",
      coordinates: {
        longitude: "",
        latitude: ""
      }
    };
  },
  name: "Switch",
  data() {
    return {
      checkbox: true,
      radioGroup: 1,
      switch1: true
    };
  },
  name: "Sidebar",
  data() {
    return {
      items: [
        { title: "Record coordinates", icon: "mdi-crosshairs-gps" },
        { title: "Map", icon: "mdi-google-maps" }
      ],
      right: null
    };
  },

  methods: {
    geo() {
      navigator.geolocation.getCurrentPosition(position => {
        lat = this.coordinates.latitude = position.coords.latitude;
        long = this.coordinates.longitude = position.coords.longitude;
        // let HTML = {
        //   accuracy: position.coords.accuracy,
        //   altitude: position.coords.altitude,
        //   altitudeAccuracy: position.coords.altitudeAccuracy,
        //   heading: position.coords.heading,
        //   latitude: position.coords.latitude,
        //   longitude: position.coords.longitude,
        //   speed: position.coords.speed
        // };
      });
    }
  }
};
</script>