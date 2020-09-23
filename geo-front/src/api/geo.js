import ApiService from './index';

const prefix = 'geo/';

const GeoApi = {
  GetMy() {
    return ApiService.get(`${prefix}GetMyExperiments`);
  },
  FixCoords(coords) {
    return ApiService.post(`${prefix}FixationPoint`, coords);
  },
  InitExperiment(title) {
    return ApiService.post(`${prefix}InitExperiment?title=${title}`);
  },
  CloseExperiment(id) {
    return ApiService.post(`${prefix}CloseExperiment?id=${id}`);
  },
};

export default GeoApi;
