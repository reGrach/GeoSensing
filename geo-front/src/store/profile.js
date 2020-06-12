/* eslint-disable */
import ProfileApi from '../api/profile';
import { GET_PROFILE, UPDATE_PROFILE, UPLOAD_AVATAR } from './actionsType';
import { SET_ERROR, SET_PROCESSING, UPLOAD_IMG } from './mutationsType';

const actions = {
  [GET_PROFILE]({ commit }) {
    commit(SET_PROCESSING, true);
    return new Promise((resolve, reject) => {
      ProfileApi.Get()
        .then(({ data }) => {
          resolve(data);
        })
        .catch(({ response }) => {
          console.log(response);
          commit(SET_ERROR, response);
          reject(response);
        })
        .finally(() => {
          commit(SET_PROCESSING, false);
        });
    });
  },
  [UPDATE_PROFILE]({ commit }, profileData) {
    commit(SET_PROCESSING, true);
    return new Promise((resolve, reject) => {
      ProfileApi.Update(profileData)
        .then(({ data }) => {
          resolve(data);
        })
        .catch(({ response }) => {
          console.log(response);
          commit(SET_ERROR, response);
          reject(response);
        })
        .finally(() => {
          commit(SET_PROCESSING, false);
        });
    });
  },
  [UPLOAD_AVATAR]({ commit }, canvas) {
    return new Promise((resolve, reject) => {
      commit(UPLOAD_IMG, true);
      canvas.toBlob((blob) => {
        ProfileApi.Upload(blob)
          .then(() => {
            resolve(); 
          })
          .catch(({ response }) => {
            commit(SET_ERROR, response);
            reject(response);
          })
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
