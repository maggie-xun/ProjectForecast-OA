import Vue from "vue";
import VueRouter from "vue-router";
import project_list from "./src/views/Project/project-list";
import project_add from "./src/views/Project/project-add";
import employee_add from "./src/views/EmployeeManagment/employee-add";
import backlog from './src/views/Summary/Backlog';
import project_detail from './src/views/Project/project-detail'
import project_edit from './src/views/Project/project-edit'
import customer_add from './src/views/Customer/customer-add'
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
  },
  {
    name: "backlog",
    path: "/backlog",
    component: backlog
  },
  {
    name: "project_detail",
    path: "/project_detail",
    component: project_detail
  },
  {
    name: "project_edit",
    path: "/project_edit",
    component: project_edit
  },
  {
    name: "customer_add",
    path: "/customer_add",
    component: customer_add
  },
];

export default new VueRouter({
  routes
});
