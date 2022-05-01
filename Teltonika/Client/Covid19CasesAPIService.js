import axios from 'axios'
import Swal from 'sweetalert2'

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
  getAll() {
    return this.execute('get', '/')
  },
  create(data) {
    if (this.checkValidation(data)) {

      return this.execute('post', '/', data)
    }
  },
  checkValidation(data) {
    if (!data.Gender) {
      Swal.fire("Enter gender")
      return;
    }
    if (!data.AgeBracket) {
      Swal.fire("Enter age bracket")
      return;
    }
    if (!data.MunicipalityCode) {
      Swal.fire("Enter municipality code")
      return;
    }
    if (!data.MunicipalityName) {
      Swal.fire("Enter municipalityname")
      return;
    }
    if (!data.ConfirmationDate) {
      Swal.fire("Enter confirmation date")
      return;
    }
    if (!data.CaseCode) {
      Swal.fire("Enter case code")
      return;
    }
    return true;
  },
}
