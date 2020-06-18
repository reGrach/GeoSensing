import ApiService from './index';

const prefix = 'team/';

const TeamApi = {
  create(team) {
    return ApiService.post(`${prefix}create`, team);
  },
  join(idTeam) {
    return ApiService.post(`${prefix}addMe`, { idTeam });
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
