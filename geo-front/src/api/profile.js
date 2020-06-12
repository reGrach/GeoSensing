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
  // Нет, надо реализовать
  Upload(file) {
    return ApiService.post(`${prefix}uploadavatar`, file);
  },
};

export default ProfileApi;
