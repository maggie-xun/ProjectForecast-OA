<template>
        <div>
            <div class="side-nav">
    
                <el-form ref="form" :model="form" label-width="120px">
                    <el-form-item label="Consultant Name" label-width="128px">
                        <el-input v-model="form.Consultant_Name"></el-input>
                    </el-form-item>
                    <el-form-item label="Type">
                        <el-select v-model="form.Type" placeholder="">
                            <el-option label="HC" value="HC"></el-option>
                            <el-option label="EX" value="EX"></el-option>
                        </el-select>
                    </el-form-item>
                    <el-form-item label="Cost Rate">
                        <el-input v-model="form.CostRate"></el-input>
                    </el-form-item>
                    <el-form-item label="Consultant Contact" label-width="142px">
                            <el-input v-model="form.Consultant_Contact"></el-input>
                        </el-form-item> 
                    <el-form-item label="Hire Decision">
                        <el-select v-model="form.HireDecision" placeholder="">
                            <el-option label="Yes" value="Yes"></el-option>
                            <el-option label="No" value="No"></el-option>
                        </el-select>
                    </el-form-item>
                    <el-form-item>
                        <el-button type="primary" @click="onSubmit">Add Now</el-button>
                        <el-button>Cancle</el-button>
                    </el-form-item>
                </el-form>
            </div>
    
        </div>
    </template>
    <script>
        // import formData_service from './../../services/form-data-service.js';
        var formData_service = require('./../../services/form-data-service');
        import axios from "axios";
        import qs from 'qs';
        export default {
            data() {
                return {
                    treeList: [
                    ],
                    form: {
                    },
                    activeName: 'first'
                }
            },
            created(){
                var _vm=this;
formData_service.default.getEmployeeById.exec(this.$route.params.item).then(data=>{
    _vm.form=data.data;
})
            },
            methods: {
                handleNodeClick(data) {
                    console.log(data);
                },
                handleClick(tab, event) {
                    console.log(tab, event);
                },
                deleteFinanceRow(index, rows, rowId) {
                    rows.splice(index, 1);
                    formData_service.default.deleteFinance.exec(rowId)
                },
                onSubmit() {
                    console.log(formData_service);
                    formData_service.default.editEmployee.extc(this.form)
                    .then(()=>{
                        this.$message('Add Successful!');
                    })
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