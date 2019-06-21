<template>
    <div>
        <div class="side-nav">
            <el-tabs v-model="activeName" type="border-card" @tab-click="handleClick" style='line-height: 40px;'>
                <el-tab-pane label="Basic Info" name="first" style='line-height: 20px;'>
                    <el-form ref="form" :model="form" label-width="120px">
                        <el-form-item label="Country">
                            <el-select v-model="form.Country" placeholder="请选择活动区域">
                                <el-option label="区域一" value="shanghai"></el-option>
                                <el-option label="区域二" value="beijing"></el-option>
                            </el-select>
                        </el-form-item>
                        <el-form-item label="Project Number">
                            <el-input v-model="form.ProjectNo"></el-input>
                        </el-form-item>
                        <el-form-item label="Project Name">
                            <el-input v-model="form.ProjectName"></el-input>
                        </el-form-item>
                        <el-form-item label="Customer">
                            <el-input v-model="form.Customer"></el-input>
                        </el-form-item>
                        <el-form-item label="Type">
                            <el-select v-model="form.Type" placeholder="">
                                <el-option label="区域一" value="shanghai"></el-option>
                                <el-option label="区域二" value="beijing"></el-option>
                            </el-select>
                        </el-form-item>
                        <el-form-item label="Status">
                            <el-select v-model="form.Status" placeholder="">
                                <el-option label="区域一" value="shanghai"></el-option>
                                <el-option label="区域二" value="beijing"></el-option>
                            </el-select>
                        </el-form-item>
                        <el-form-item label="CloseDate">
                            <div class="block">
                                <el-date-picker v-model="form.CloseDate" type="date" placeholder="">
                                </el-date-picker>
                            </div>
                        </el-form-item>
                        <el-form-item>
                                <el-button type="primary" @click="onSubmit">Add Now</el-button>
                                <el-button>Next Page</el-button>
                              </el-form-item>
                    </el-form>
                </el-tab-pane>
                <el-tab-pane label="Utilization" name="second">配置管理</el-tab-pane>
                <el-tab-pane label="FinancialReport" name="third">角色管理</el-tab-pane>
            </el-tabs>
        </div>

    </div>
</template>
<script>
    // import formData_service from './../../services/form-data-service.js';
    var formData_service=require('./../../services/form-data-service');
    import axios from "axios";
    import qs from 'qs';
    export default {
        data() {
            return {
                treeList: [
                   ],
                form: {
                    name: ","
                },
                activeName: 'first'
            }
        },
        methods: {
            handleNodeClick(data) {
                console.log(data);
            },
            handleClick(tab, event) {
                console.log(tab, event);
            },
            onSubmit(){
                console.log(formData_service);
                formData_service.default.upload.extc(this.form)
            }
        }
    }
</script>
<style>
    .side-nav {
        height: 100%;
        padding-left: 20px;
        padding-right: 0;
    }

    .side-nav>ul {
        padding-bottom: 50px;
    }

    .side-nav li {
        list-style: none;
        height: 40p;
    }
</style>