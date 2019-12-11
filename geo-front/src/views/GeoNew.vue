<template>
  <v-content>
    <v-container fluid fill-height>
      <v-layout align-center justify-center>
        <v-flex xs12 sm8 md4>
          <v-card class="elevation-12">
            <v-toolbar color="primary" dark flat>
              <v-switch v-model="flagAccuracy" label="Высокая точность"></v-switch>
              <v-spacer></v-spacer>
              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn color="blue" @click="geo">Получить</v-btn>
              </v-card-actions>
            </v-toolbar>

            <v-card-text>
              <v-form>
                <v-text-field label="accuracy" type="text" v-model="geoInfo.accuracy"></v-text-field>
                <v-text-field label="altitude" type="text" v-model="geoInfo.altitude"></v-text-field>
                <v-text-field
                  label="altitudeAccuracy"
                  type="text"
                  v-model="geoInfo.altitudeAccuracy"
                ></v-text-field>
                <v-text-field label="heading" type="text" v-model="geoInfo.heading"></v-text-field>
                <v-text-field label="latitude" type="text" v-model="geoInfo.latitude"></v-text-field>
                <v-text-field label="longitude" type="text" v-model="geoInfo.longitude"></v-text-field>
                <v-text-field label="speed" type="text" v-model="geoInfo.speed"></v-text-field>
                <v-text-field label="error" type="text" v-model="errorr"></v-text-field>
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
      flagAccuracy: false,
      errorr: "",
      geoInfo: {
        accuracy: "",
        altitude: "",
        altitudeAccuracy: "",
        heading: "",
        latitude: "",
        longitude: "",
        speed: ""
      }
    };
  },
  methods: {

    geo_success(position) {
        console.log(position);
        alert("Получил позицию")
        this.geoInfo.accuracy = position.coords.accuracy;
        this.geoInfo.altitude = position.coords.altitude;
        this.geoInfo.altitudeAccuracy = position.coords.altitudeAccuracy;
        this.geoInfo.heading = position.coords.heading;
        this.geoInfo.latitude = position.coords.latitude;
        this.geoInfo.longitude = position.coords.longitude;
        this.geoInfo.speed = position.coords.speed;
      },





    
    geo() {
      
      function geo_error(err) {
        console.log("Ошибка");
        console.log(err);
        alert(err.message);
      }

      let settings = {
        enableHighAccuracy: this.flagAccuracy,
        maximumAge: 30000,
        timeout: 27000
      };
      var res = navigator.geolocation.getCurrentPosition(this.geo_success, geo_error, settings);

      console.log(res);
      // (position, error, settings) => {
      //   if (position) {
      //     console.log(position);
      //     console.log(settings);
      //     this.geoInfo.accuracy = position.coords.accuracy;
      //     this.geoInfo.altitude = position.coords.altitude;
      //     this.geoInfo.altitudeAccuracy = position.coords.altitudeAccuracy;
      //     this.geoInfo.heading = position.coords.heading;
      //     this.geoInfo.latitude = position.coords.latitude;
      //     this.geoInfo.longitude = position.coords.longitude;
      //     this.geoInfo.speed = position.coords.speed;
      //   } else {
      //     console.log(error);
      //     this.errorr = error;
      //   }
      // });
    }
  }
};
</script>