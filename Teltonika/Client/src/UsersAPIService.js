import axios from 'axios'
import Swal from 'sweetalert2'
import router from './router'

const client = axios.create({
  baseURL: 'http://localhost:39710/api/Users',
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
  create(data) {
    var valid = this.checkValidationSignup(data)
    if (valid) {
      this.execute('post', '/', data)
        .then(response => {
          if (response.Id > 0) {
            Swal.fire("Successfully registered")
              .then(() => {
                router.push({ name: "Login" });
              })
          }
          else {
            Swal.fire("Unexpected error. Please try again.")
          }
        })
        .catch(error => {
          if (error.response.status == 403) {
            Swal.fire("User with provided username already exists")
          }
          if (error.reponse) {
            Swal.fire(error.response.data)
          }
        });
    }
    this.data = {}
  },
  get(data) {
    var successfulLogin = false
    if (this.checkValidationLogin(data)) {
      axios.get("http://localhost:39710/api/Users/Signin/" + data.username + "/" + data.password)
        .then(response => {
          if (response.status == 200) {
            localStorage.setItem('token', JSON.stringify(response.data.token));
            response.data.token = "";
            successfulLogin = true
            localStorage.setItem('user', JSON.stringify(response.data));
            router.push({ name: "Covid19Cases" });
            router.go(0);
          }
          response => response
        })
        .catch(error => {
          if (error.reponse) {
            Swal.fire(error.response.data)
          }
          Swal.fire("Login attempt was unsuccessful. Make sure that username and password are correct.")
        })
    }
  },
  checkValidationLogin(data) {
    if (!data.username) {
      Swal.fire("Enter username")
      return;
    }
    if (!data.password) {
      Swal.fire("Enter password")
      return;
    }
    return true;
  },
  checkValidationSignup(data) {
    if (!data.fullname) {
      Swal.fire("Enter password")
      return false
    }
    else if (!data.username) {
      Swal.fire("Enter username")
      return false
    }
    else if (!data.password) {
      Swal.fire("Enter password")
      return false
    }

    return true
  },
}
