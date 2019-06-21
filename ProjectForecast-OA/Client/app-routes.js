import Vue from 'vue';
import VueRouter from "vue-router";
import project_list from './src/views/Project/project-list';
import employee_add from './src/views/Project/employee-add'
Vue.use(VueRouter);

const routes = [

{
  name:'project_list',
path:'/project_list',
component:project_list
},{
  name:'employee_add',
path:'/employee_add',
component:employee_add
}

];

export default new VueRouter({
    routes
});
