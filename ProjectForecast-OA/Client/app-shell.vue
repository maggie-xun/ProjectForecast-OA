<template>
    <div>
        <el-container>
            <el-header style="line-height: 60px;">
                <el-menu :default-active="activeIndex2" class="el-menu-demo" mode="horizontal" @select="handleSelect"
                    background-color="#545c64" text-color="#fff" active-text-color="#ffd04b">
                    <el-submenu index="project_list">
                        <template slot="title">Project Forecast</template>
                        <el-menu-item index="project_list">
                            <router-link to="/project_list">Project List</router-link>
                        </el-menu-item>

                        <template slot="title"> </template>
                        <el-menu-item index="employee_add">
                            <router-link to="/employee_add">Add Project</router-link>
                        </el-menu-item>
                        <template slot="title"> </template>
                        <el-menu-item index="employee_add">
                            <router-link to="/employee_add">Add From Excel</router-link>
                        </el-menu-item>
                    </el-submenu>
                    <el-submenu index="2">
                        <template slot="title">Employee Managment</template>
                        <el-submenu index="2-4">
                            <template slot="title">
                                <el-menu-item index="employee_add">
                                    <router-link to="/employee_add">Add Empoyee</router-link>
                                </el-menu-item>
                            </template>
                        </el-submenu>
                    </el-submenu>
                    <el-menu-item index="3" disabled>CRM</el-menu-item>
                    <el-menu-item index="4"><a href="https://www.ele.me" target="_blank">Overview</a></el-menu-item>
                    <el-menu-item index="5"><a href="https://www.ele.me" target="_blank">SAP LTF</a></el-menu-item>
                    <el-menu-item index="6" style="float: right"><a href="/Account/LogOff">Log Off</a></el-menu-item>
                </el-menu>
            </el-header>
            <!-- <el-container>
                <el-aside width="200px">Aside</el-aside>
                <el-main>Main</el-main>
            </el-container> -->
            <router-view></router-view>
        </el-container>

    </div>
</template>
<script>
    var project_list = require("./src/views/Project/project-list").default;

    export default {

        data: function () {
            return {
                "activeName": 'first',
                activeIndex2: 'project_list'
            }
        },
        created() {
            this.handleSelect("project_list");
        },
        components: {
            'project_list': project_list
        },
        methods: {
            handleClick(tab, event) {
                console.log(tab, event);
            },
            handleSelect(key, keyPath) {
                console.log(key, keyPath);
                // console.log(this.$router);
                this.$router.push({
                    name: key
                })
            }
        },
    }
</script>
<style scoped>

</style>