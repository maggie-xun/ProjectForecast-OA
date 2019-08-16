<template>
    <div>
        <div class="side-nav">

            <el-form ref="form" :model="form" label-width="120px">
                <el-form-item label="Consultant Name" label-width="128px">
                    <el-input v-model="form.Consultant_Name"></el-input>
                </el-form-item>
                <el-form-item label="Work Type">
                    <el-select v-model="form.WorkType" placeholder="">
                        <el-option label="Absent" value="Absent"></el-option>
                        <el-option label="Presales" value="Presales"></el-option>
                        <el-option label="Bench" value="Bench"></el-option>
                        <el-option label="Project" value="Project"></el-option>
                    </el-select>
                </el-form-item>

                <el-form-item label="Project" v-if="form.WorkType=='Project'">
                    <el-input v-model="form.ProjectId"></el-input>
                </el-form-item>
                <el-form-item label="Consultant Contact" label-width="142px">
                    <el-input v-model="form.Consultant_Contact"></el-input>
                </el-form-item>
               
            </el-form>

            <!-- <div class="block">
                <el-table border :data="sumList" style="width: 100%">
                    <el-table-column  :label="i" style="width:6vw;" v-for="i in 30">
                        <template scope="scope">
                            <el-input size="mini" v-model="scope.row.Day"></el-input>
                        </template>
                    </el-table-column>                 
                </el-table>
            </div> -->
        </div>

    </div>
</template>
<script type="text/javascript" src="./../../lib/vue.js"></script>
<script type="text/javascript" src="./../../lib/elementui/index.js"></script>
<script>
    //import formData_service from './../../services/form-data-service.js';
    var formData_service = require('./../../services/form-data-service');
    // var dateMultiElementUIIndex=require('./../../lib/elementui/index.js');
    var dateMultiVue=require('./../../lib/vue.js');
    import axios from "axios";
    import qs from 'qs';
    export default {
        data() {
            return {
                treeList: [
                ],
                form: {
                },
                activeName: 'first',
                dateArr: [],
                printStr: "",
                sumList:[]
            }
        },
        created() {
            var _vm = this;
            formData_service.default.getEmployeeById.exec(3).then(data => {
                _vm.form = data.data;
            })
        },
        mounted: function(){
          //为了解决bug，所以默认值放在了这里
          this.$nextTick(function(){
            this.dateArr = [];
            this.$refs.datesRef.showPicker();
            this.$refs.datesRef.hidePicker();
          });
        },
        methods: {
            deleteFinanceRow(index, rows, rowId) {
                rows.splice(index, 1);
                formData_service.default.deleteFinance.exec(rowId)
            },
            onSubmit() {
                formData_service.default.editEmployee.extc(this.form)
                    .then(() => {
                        this.$message('Add Successful!');
                        this.$router.push({
                            name: 'employee_list',
                        })
                    })
            },
            clickBtn: function () {
                this.printStr = this.dateArr ? this.dateArr.join() : "";
                console.log(this.printStr);
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