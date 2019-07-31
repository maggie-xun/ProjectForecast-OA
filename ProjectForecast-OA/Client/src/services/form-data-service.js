import axios from "axios"; //引入
let host ='';
export default {
  upload: {
    url() {
      return `${host}/ProjectForecast/AddProject`;
    },
    extc: function(form) {
      axios.post(this.url(), form).then(data => {
        console.log(data);
      });
    }
  },
  getProjects: {
    url() {
      return `${host}/ProjectForecast/GetProjects`;
    },
    extc: function() {
      return axios.get(this.url());
      //   .then((data)=>{
      //       return data.data;
      //   })
    }
  },
  getProjectPerId: {
    url() {
      return `${host}/ProjectForecast/GetProjectPerId`;
    },
    exec: function(projectNo) {
      var _self = this;

      return axios.get(this.url(), {
        params: {
          id: projectNo
        }
      });

      //   .then((data)=>{
      //       return data.data;
      //   })
    }
  },
  getAllEmployee: {
    url() {
      return `${host}/ProjectForecast/GetAllEmployees`;
    },
    extc: function() {
      return axios.get(this.url());
      //   .then((data)=>{
      //       return data.data;
      //   })
    }
  },
  addEmployee: {
    url() {
      return `${host}/ProjectForecast/AddEmployee`;
    },
    extc: function(form) {
      return new Promise((resolve, reject) => {
        axios.post(this.url(), form)
          .then(res => {
            resolve(res);
          })
          .catch(err => {
            reject(err);
          });
      });
    }
  },

  addCustomer:{
    url() {
      return `${host}/ProjectForecast/AddCustomer`;
    },
    extc: function(form) {
      return new Promise((resolve, reject) => {
        axios.post(this.url(), form)
          .then(res => {
            resolve(res);
          })
          .catch(err => {
            reject(err);
          });
      });
    }
  },
  addProject: {
    url() {
      return `${host}/ProjectForecast/AddProject`;
    },
    extc: function(form) {
      axios.post(this.url(), form).then(data => {
        console.log(data);
      });
    }
  },
  editProject: {
    url() {
      return `${host}/ProjectForecast/EditProject`;
    },
    extc: function(form) {
      return new Promise((resolve, reject) => {
        axios.post(this.url(), form)
          .then(res => {
            resolve(res);
          })
          .catch(err => {
            reject(err);
          });
      });
    }
  },
  getAllCountry: {
    url() {
      return `${host}/ProjectForecast/GetAllCountry`;
    },
    extc: function() {
      return axios.get(this.url());
      //   .then((data)=>{
      //       return data.data;
      //   })
    }
  },

  getAllCustomer:{
    url() {
      return `${host}/ProjectForecast/GetAllCustomer`;
    },
    exec: function() {
      return axios.get(this.url());
      //   .then((data)=>{
      //       return data.data;
      //   })
    }
  },

  addCountry: {
    url() {
      return `${host}/ProjectForecast/AddCountry`;
    },
    extc: function(country) {
      axios.post(this.url(), country).then(data => {
        console.log(data);
      });
    }
  },

  deleteEmployeeWorkdayDetail:{
    url() {
      return `${host}/ProjectForecast/DeleteEmployeeWorkdayDetail`;
    },
    exec: function(id) {
      return axios.get(this.url(), {
        params: {
          id: id
        }
      });
    }
  },
  deleteFinance:{
    url() {
      return `${host}/ProjectForecast/DeleteFinance`;
    },
    exec: function(id) {
      return axios.get(this.url(), {
        params: {
          id: id
        }
      });
    }
  },
  deleteEmployee:{
    url() {
      return `${host}/ProjectForecast/DeleteEmployee`;
    },
    exec: function(id) {
      return axios.get(this.url(), {
        params: {
          id: id
        }
      });
    }
  },
  getEmployeeById:{
    url() {
      return `${host}/ProjectForecast/GetEmployeePerId`;
    },
    exec: function(projectNo) {
      var _self = this;

      return axios.get(this.url(), {
        params: {
          id: projectNo
        }
      });
    }
  },
  editEmployee:{
    url() {
      return `${host}/ProjectForecast/EditEmployee`;
    },
    extc: function(form) {
      return new Promise((resolve, reject) => {
        axios.post(this.url(), form)
          .then(res => {
            resolve(res);
          })
          .catch(err => {
            reject(err);
          });
      });
    }
  },

  getEmployeeById:{
    url() {
      return `${host}/ProjectForecast/GetCustomerPerId`;
    },
    exec: function(projectNo) {
      var _self = this;

      return axios.get(this.url(), {
        params: {
          id: projectNo
        }
      });
    }
  },
  deleteCustomer:{
    url() {
      return `${host}/ProjectForecast/DeleteCustomer`;
    },
    exec: function(id) {
      return axios.get(this.url(), {
        params: {
          id: id
        }
      });
    }
  },
  editCustomer:{
    url() {
      return `${host}/ProjectForecast/EditCustomer`;
    },
    extc: function(form) {
      return new Promise((resolve, reject) => {
        axios.post(this.url(), form)
          .then(res => {
            resolve(res);
          })
          .catch(err => {
            reject(err);
          });
      });
    }
  },
};
//然后再修改原型链
// Vue.prototype.$http = axios;
