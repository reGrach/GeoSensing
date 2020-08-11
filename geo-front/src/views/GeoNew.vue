<template>
  <v-layout style="color:white;" align-left justify-left pa-7>
    <v-card flat width="100%" tile text-align="left">
      <v-switch v-model="switch1" :label="`Mode: ${switch1.toString()}`"></v-switch>
      <v-form>
        <v-text-field id="lat" label="latitude" name="lat" type="text" v-model="coordinates">{{latitude}}</v-text-field>
        <v-text-field id="long" label="longitude" name="long" type="text" v-model="coordinates">{{longitude}}</v-text-field>
        <v-text-field id="alt" label="altitude" name="alt" type="text" v-model="coordinates">{{altitude}}</v-text-field>
      </v-form>
      <v-btn color="blue">Submit</v-btn>
      <v-btn color="blue" @click="geo">get Current Position</v-btn>
    </v-card>
  </v-layout>
</template>

<script>
export default {
  name: "Geo",
  data() {
    return {
      namebutton: "get current position",
      coordinates: {
        latitude: '',
        longitude: '',
        altitude: ''
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
  methods: {
    geo() {
      navigator.geolocation.getCurrentPosition(position => {
        this.coordinates.latitude = position.coordinates.latitude;
        this.coordinates.longitude = position.coordinates.longitude;
        this.coordinates.altitude = position.coordinates.altitude;
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