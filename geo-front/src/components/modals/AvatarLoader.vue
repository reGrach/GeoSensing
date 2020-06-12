<template >
  <v-dialog
    :fullscreen="$vuetify.breakpoint.xs"
    width="720"
    transition="dialog-bottom-transition"
    v-model="show"
  >
    <v-card raise>
      <v-toolbar dense dark color="primary">
        <v-btn icon dark @click="show = false">
          <v-icon>mdi-close</v-icon>
        </v-btn>
        <v-toolbar-title>Новый аватар</v-toolbar-title>
        <v-spacer></v-spacer>
      </v-toolbar>
      <v-card-text>
        <v-file-input
          class="my-4"
          v-model="selectedFile"
          :rules="rules"
          accept="image/png, image/jpeg, image/bmp"
          placeholder="Выбрать изображение"
          prepend-icon="mdi-camera"
          label="Аватар"
          :show-size="1024"
          @change="setupCropper"
        ></v-file-input>
        <v-row v-if="objectUrl">
          <v-col cols="12" sm="6" class="text-center">
            <div class="overline">Original</div>
            <div class="image-container elevation-4">
              <img class="image-preview" ref="source" :src="objectUrl"/>
            </div>
            <div class="d-flex justify-center" >
              <v-btn icon small @click="resetCropper">
                <v-icon small>mdi-aspect-ratio</v-icon>
              </v-btn>
              <div>
                <v-btn icon small @click="rotateLeft">
                  <v-icon small>mdi-rotate-left</v-icon>
                </v-btn>
                <v-btn icon small @click="rotateRight">
                  <v-icon small>mdi-rotate-right</v-icon>
                </v-btn>
              </div>
            </div>
          </v-col>
          <v-col cols="12" sm="6" class="text-center">
            <div class="overline">Preview</div>
            <div class="image-container elevation-4">
              <img class="image-preview" :src="previewCropped"/>
            </div>
          </v-col>
        </v-row>
      </v-card-text>
      <v-card-actions>
        <v-spacer />
        <v-btn color="primary" :disabled="!objectUrl" @click="saveAvatar" :loading="isUpload">
          <span>Сохранить</span>
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
// import debounce from 'lodash/debounce'
import Cropper from 'cropperjs';
import debounce from 'lodash.debounce';
import 'cropperjs/dist/cropper.css';
import { UPLOAD_AVATAR } from '@/store/actionsType';
import { mapGetters } from 'vuex';

export default {
  props: {
    value: Boolean,
  },
  data() {
    return {
      isValid: false,
      cropper: null,
      objectUrl: null,
      previewCropped: null,
      selectedFile: null,
      debouncedUpdatePreview: debounce(this.updatePreview, 300),
      rules: [
        (v) => !v || v.size < 2000000 || 'Размер файла не должен превышать 2 MB!',
      ],
    };
  },
  computed: {
    ...mapGetters(['getError', 'isUpload']),
    show: {
      get() {
        return this.value;
      },
      set(value) {
        this.$emit('input', value);
      },
    },
  },
  methods: {
    resetCropper() {
      this.cropper.reset();
    },
    rotateLeft() {
      this.cropper.rotate(-90);
    },
    rotateRight() {
      this.cropper.rotate(90);
    },
    setupCropper(selectedFile) {
      if (this.cropper) {
        this.cropper.destroy();
      }

      if (this.objectUrl) {
        window.URL.revokeObjectURL(this.objectUrl);
      }

      if (!selectedFile) {
        this.cropper = null;
        this.objectUrl = null;
        this.previewCropped = null;
        return;
      }

      this.objectUrl = window.URL.createObjectURL(selectedFile);
      this.$nextTick(() => {
        this.cropper = new Cropper(this.$refs.source, {
          aspectRatio: 1,
          crop: this.debouncedUpdatePreview,
        });
      });
    },
    updatePreview() {
      const canvas = this.cropper.getCroppedCanvas();
      this.previewCropped = canvas.toDataURL('image/png');
    },
    saveAvatar() {
      const canvas = this.cropper.getCroppedCanvas();
      this.$store.dispatch(UPLOAD_AVATAR, canvas)
        .then(() => {
          this.show = false;
          this.$emit('uploaded');
        });
    },
  },
};
</script>

<style lang="scss" scoped>
.image-container {
  display: inline-block;
}
.image-preview {
  display: block;
  max-height: 229px;
  max-width: 100%;
}
</style>
