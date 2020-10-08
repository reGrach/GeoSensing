import ApiService from './index';

const prefix = 'team/';

const TeamApi = {
  create(team) {
    return ApiService.post(`${prefix}create`, team);
  },
  join(TeamId) {
    return ApiService.post(`${prefix}addMe`, { TeamId });
  },
  leave() {
    return ApiService.post(`${prefix}removeMe`);
  },
  exclude(userLogin) {
    return ApiService.post(`${prefix}removeUser`, { userLogin });
  },
  getAll(isAdmin) {
    const route = isAdmin ? 'getAll' : 'getActive';
    return ApiService.get(`${prefix}${route}`);
  },
  get(id) {
    const route = id ? `getById/${id}` : 'getMy';
    return ApiService.get(`${prefix}${route}`);
  },
};

export default TeamApi;
