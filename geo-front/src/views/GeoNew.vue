<template>
  <v-content>
    <v-container fluid fill-height>
      <v-layout align-center justify-center>
        <v-flex xs12 sm8 md4>
          <v-card class="elevation-12">
            <v-toolbar color="primary" dark flat>
              <v-toolbar-title>Получение позиции</v-toolbar-title>
              <v-spacer></v-spacer>
              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn @click="geo" color="blue">GetCurPos</v-btn>
              </v-card-actions>
            </v-toolbar>
            <v-card-text>
              <v-form>
                <v-text-field label="lattitude" name="lat" type="text">{{lat}}</v-text-field>

                <v-text-field
                  id="long"
                  label="longitude"
                  name="long"
                  type="text"
                >{{long}}</v-text-field>
              </v-form>
            </v-card-text>
          </v-card>
        </v-flex>
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
  methods: {
    geo() {
      navigator.geolocation.getCurrentPosition(position => {
        this.coordinates.latitude = position.coords.latitude;
        this.coordinates.longitude = position.coords.longitude;
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