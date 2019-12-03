<template>
<v-app>
  <div>
    <h1 align="center">Get your current location in different ways!</h1>
    <div style="text-align: center; padding-bottom: 20px;">
      <div style="margin-bottom:20px;">
        <button @click="geo">{{namebutton}}</button>
        <!-- <input id="getcurpos" type="button" value="get current position" onclick="getcurpos" /> -->
        <!-- <input id="watpos" type="button" value="watch position" onclick="watpos" /> -->
        <input id="stopwatpos" type="button" value="stop watching position" onclick="stopwatpos" />
      </div>
      <form method="post" action="/sendpos">
        <b>датчик:</b>
        <input type="text" v-model="coordinates.latitude" />
        <input type="text" v-model="coordinates.longitude" />
        <input type="text" id="mark" name="mark" value />
        <button type="submit">Submit</button>
      </form>
    </div>
    <div style="text-align: center">
      <b>get point by mark:</b>
      <form method="post" action="/getpos">
        <input type="text" id="getpos" name="mark" value />
        <button type="submit">GetPos</button>
      </form>
    </div>
  </div>
</v-app>
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