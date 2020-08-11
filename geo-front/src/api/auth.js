import ApiService from './index';

const prefix = 'auth/';

const AuthApi = {
  signin(user) {
    return ApiService.post(`${prefix}signin`, user);
  },
  signup(user) {
    return ApiService.post(`${prefix}signup`, user);
  },
  signout() {
    return ApiService.post(`${prefix}signout`);
  },
  check() {
    return ApiService.get(`${prefix}check`);
  },
};

export default AuthApi;
