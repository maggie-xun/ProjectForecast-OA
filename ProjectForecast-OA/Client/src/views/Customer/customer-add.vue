<template>
        <div>
            <div class="side-nav"> 
                <el-form ref="form" :model="form" label-width="120px">
                    <el-form-item label="Customer">
                        <el-input v-model="form.Customer_Name"></el-input>
                    </el-form-item>
                    <el-form-item label="Cutomer Contact" label-width="128px">
                        <el-input v-model="form.Customer_Contact"></el-input>
                    </el-form-item>
                    <el-form-item>
                        <el-button type="primary" @click="onSubmitCustomer">Add Now</el-button>
                            <el-button>Cancle</el-button>
                     </el-form-item>
                </el-form>
            </div>
    
        </div>
    </template>
    <script>
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
            methods: {
                handleNodeClick(data) {
                    console.log(data);
                },
                handleClick(tab, event) {
                    console.log(tab, event);
                },
                onSubmitCustomer() {
                    formData_service.default.addCustomer.extc(this.form)
                    .then(()=>{
                        this.$message('Add Successful!');
                        this.$router.push({
                                      name: 'customer_list',
                                  })
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