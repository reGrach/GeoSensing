/* eslint-disable */
import ProfileApi from '../api/profile';
import { GET_PROFILE, UPDATE_PROFILE, UPLOAD_AVATAR } from './actionsType';
import { SET_PROCESSING, UPLOAD_IMG, SET_AVA, SHOW_PRELOADER, HIDE_PRELOADER, UPDATE_NOTIFY } from './mutationsType';

const actions = {
  [GET_PROFILE]({ commit }, id) {
    commit(SHOW_PRELOADER);
    return new Promise((resolve, reject) => {
      ProfileApi.Get(id)
        .then(({ data }) => {
          commit(SET_AVA, data.avatarSrc)
          resolve(data);
        })
        .catch(() => reject())
        .finally(() => {
          commit(HIDE_PRELOADER);
        });
    });
  },

  [UPDATE_PROFILE]({ commit }, profileData) {
    commit(SET_PROCESSING, true);
    ProfileApi.Update(profileData)
    .then(() => {
      commit(UPDATE_NOTIFY, {isError: false, msg: 'Данные обновлены'})
    })
    .catch(() => {
      commit(UPDATE_NOTIFY, {isError: true, msg: 'Ошибка обнолвения данных'})
    })
    .finally(() => {
      commit(SET_PROCESSING, false);
    });

  },

  [UPLOAD_AVATAR]({ commit }, canvas) {
    return new Promise((resolve, reject) => {
      commit(UPLOAD_IMG, true);
      canvas.toBlob((blob) => {
        ProfileApi.Upload(blob)
        .then(() => {resolve();})
        .catch(() => {reject();})
        .finally(() => {
          commit(UPLOAD_IMG, false);
        });
      })
    })
  }
};

export default {
  actions,
};
