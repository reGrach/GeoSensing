module.exports = {
  lintOnSave: process.env.NODE_ENV !== 'production',
  transpileDependencies: [
    'vuetify',
  ],
  outputDir: process.env.NODE_ENV === 'production'
    ? '/var/www/geo_sensing/site'
    : 'dist',
};
