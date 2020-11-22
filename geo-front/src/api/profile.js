import ApiService from './index';

const prefix = 'profile/';

const ProfileApi = {
  Get(id) {
    const route = id ? `getById/${id}` : 'get';
    return ApiService.get(`${prefix}${route}`);
  },
  Search(query) {
    return ApiService.query(`${prefix}search`, query);
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
