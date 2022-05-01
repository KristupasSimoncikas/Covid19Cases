import axios from 'axios'
const client = axios.create({
  baseURL: 'http://localhost:39710/api/Covid19Cases',
  json: true
})

export default {
  async execute(method, resource, data) {
    return client({
      method,
      url: resource,
      data,
    }).then(req => {
      return req.data
    })
  },
  getChartData(group) {
    return this.execute('get', '/' + group)
  },
}
