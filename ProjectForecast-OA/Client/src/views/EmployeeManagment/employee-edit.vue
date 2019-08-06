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
                <el-form-item label="Time" label-width="142px">
                    <el-date-picker
                    ref="datesRef"
                    type="dates"
                    v-model="dateArr"
                    :editable = "false"
                    format="yyyy-MM-dd"
                    value-format="yyyy-MM-dd"
                    placeholder="选择一个或多个日期">
                  </el-date-picker>
                </el-form-item>
                <el-form-item label="Hire Decision">
                    <el-select v-model="form.HireDecision" placeholder="">
                        <el-option label="Yes" value="Yes"></el-option>
                        <el-option label="No" value="No"></el-option>
                    </el-select>
                </el-form-item>
                <el-form-item>
                        <el-button type="primary" @click="clickBtn" class="btn">打印选择的时间</el-button>
                    <el-button type="primary" @click="onSubmit">Add Now</el-button>
                    <el-button>Cancle</el-button>
                </el-form-item>
            </el-form>
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
                printStr: ""
            }
        },
        created() {
            var _vm = this;
            formData_service.default.getEmployeeById.exec(this.$route.params.item).then(data => {
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