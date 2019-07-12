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
  getProjects:{
    url() {
        return `${host}/ProjectForecast/GetProjects`;
      },
      extc:function(){
         return axios.get(this.url())
        //   .then((data)=>{
        //       return data.data;
        //   })
      }
  },
  getAllEmployee:{
    url() {
        return `${host}/ProjectForecast/GetAllEmployees`;
      },
      extc:function(){
         return axios.get(this.url())
        //   .then((data)=>{
        //       return data.data;
        //   })
      }
  },
  addEmployee:{
      url(){
          return `${host}/ProjectForecast/AddEmployee`;
      },
      extc: function(form) {
        axios.post(this.url(), form).then(data => {
          console.log(data);
        });
      }
  },

  addProject:{
    url(){
        return `${host}/ProjectForecast/AddProject`;
    },
    extc: function(form) {
      axios.post(this.url(), form).then(data => {
        console.log(data);
      });
    }
  },
  getAllCountry:{
    url() {
        return `${host}/ProjectForecast/GetAllCountry`;
      },
      extc:function(){
         return axios.get(this.url())
        //   .then((data)=>{
        //       return data.data;
        //   })
      }
  },

  addCountry:{
    url(){
        return `${host}/ProjectForecast/AddCountry`;
    },
    extc: function(country) {
      axios.post(this.url(), country).then(data => {
        console.log(data);
      });
    }
  }
};
//然后再修改原型链
// Vue.prototype.$http = axios;
