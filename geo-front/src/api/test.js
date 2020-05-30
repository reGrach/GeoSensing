import ApiService from './index'

const prefix = 'test/'

const TestApi = {
  default () {
    return ApiService.get(prefix + 'testApi')
  }
}

export default TestApi
