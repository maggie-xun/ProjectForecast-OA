import Polyfill from 'babel-polyfill';
import Vue from 'vue';

import ElementUI from 'element-ui';
import './node_modules/element-ui/lib/theme-chalk/index.css';
import './app.css'
import VueRouter from 'vue-router';
import shell from './app-shell';
import router from './app-routes';

Vue.use(ElementUI);

const app = new Vue({
    el: '#vue-container',
    render: createElement => createElement(shell),
    router,
});