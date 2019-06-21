import axios from "axios"; //引入
let host = "http://localhost:44364";
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
  }
};
//然后再修改原型链
// Vue.prototype.$http = axios;
