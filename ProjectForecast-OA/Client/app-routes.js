import Vue from "vue";
import VueRouter from "vue-router";
import project_list from "./src/views/Project/project-list";
import project_add from "./src/views/Project/project-add";
import project_addFromExcel from "./src/views/Project/project-addFromExcel";
import employee_list from "./src/views/EmployeeManagment/employee-list";
import employee_add from "./src/views/EmployeeManagment/employee-add";
import employee_edit from './src/views/EmployeeManagment/employee-edit'
import employee_timesheet from './src/views/EmployeeManagment/employee-timesheet'
import backlog from './src/views/Summary/Backlog';
import project_detail from './src/views/Project/project-detail'
import project_edit from './src/views/Project/project-edit'
import customer_add from './src/views/Customer/customer-add'
import customer_list from './src/views/Customer/customer-list'
import customer_edit from './src/views/Customer/customer-edit'


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
    name: "employee_list",
    path: "/employee_list",
    component: employee_list
  },
  {
    name: "employee_edit",
    path: "/employee_edit",
    component: employee_edit
  },
  {
    name: "employee_timesheet",
    path: "/employee_timesheett",
    component: employee_timesheet
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
    name: "project_addFromExcel",
    path: "/project_addFromExcel",
    component: project_addFromExcel
  },
  {
    name: "customer_add",
    path: "/customer_add",
    component: customer_add
  },
  {
    name: "customer_list",
    path: "/customer_list",
    component: customer_list
  },
  {
    name: "customer_edit",
    path: "/customer_edit",
    component: customer_edit
  },

  
];

export default new VueRouter({
  routes
});
