import axios from "axios";
Vue.prototype.$axios = axios;
// let host = "http://localhost:44364";
// let _svc = {
//   addProject: {
//     url() {
//       return `${host}/ProjectForecast/AddProject`;
//     },
//     extc: function() {
//       var _self = this,
//         deferred = $.Deferred();
//       this.$axios
//         .get(url, {
//           params: {}
//         })
//         .then(res => {})
//         .catch(err => {});
//     }
//   }
// };

// modules.exports = _svc;
