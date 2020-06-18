import ApiService from './index';

const prefix = 'profile/';

const ProfileApi = {
  GetById(id) {
    return ApiService.get(`${prefix}getbyid/${id}`);
  },
  Get() {
    return ApiService.get(`${prefix}get`);
  },
  Search(query) {
    return ApiService.get(`${prefix}search`, query);
  },
  Update(profile) {
    return ApiService.post(`${prefix}update`, profile);
  },
  Upload(blob) {
    const formData = new FormData();
    formData.append('Avatar', blob, 'Avatar.png');
    return ApiService.post(`${prefix}uploadavatar`, formData, {
      headers: {
        'Content-Type': 'multipart/form-data',
      },
    });
  },
};

export default ProfileApi;
