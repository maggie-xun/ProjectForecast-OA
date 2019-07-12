import Vue from "vue";
import VueRouter from "vue-router";
import project_list from "./src/views/Project/project-list";
import project_add from "./src/views/Project/project-add";
import employee_add from "./src/views/EmployeeManagment/employee-add";
Vue.use(VueRouter);

const routes = [
  {
    name: "project_list",
    path: "/project_list",
    component: project_list
  },
  {
    name: "project_add",
    path: "/project_add",
    component: project_add
  },
  {
    name: "employee_add",
    path: "/employee_add",
    component: employee_add
  }
];

export default new VueRouter({
  routes
});
