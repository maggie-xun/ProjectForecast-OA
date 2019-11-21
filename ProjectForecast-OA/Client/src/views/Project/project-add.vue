<template>
    <div>
        <div class="side-nav">
            <el-steps :active="active" finish-status="success" style="margin-top:20px">
                <el-step title="Basic Info"></el-step>
                <el-step title="Utilization"></el-step>
                <el-step title="FinancialReport"></el-step>
            </el-steps>
        <div v-show="active==0">
            <el-form ref="form" :model="form" label-width="120px">
                <el-form-item label="Country">
                    <el-select v-model="form.Country.CountryId" placeholder="Please Select a Country">
                        <el-option :label="country.CountryName" :value="country.CountryId" v-for='country in Country'>
                        </el-option>
                    </el-select>
                    <el-button type="text" @click="addCountry">Add Country</el-button>
                </el-form-item>
                <el-form-item label="Project Number">
                    <el-input v-model="form.ProjectNo"></el-input>
                </el-form-item>
                <el-form-item label="Project Name">
                    <el-input v-model="form.ProjectName"></el-input>
                </el-form-item>
                <el-form-item label="Project Manager" label-width="121px">
                    <el-select v-model="form.Consultant.Consultant_Id" placeholder="">
                        <el-option :label="consultant.Consultant_Name" :value="consultant.Consultant_Id"
                            v-for='consultant in consultants'></el-option>
                    </el-select>
                </el-form-item>
                <el-form-item label="Customer">
                    <el-select v-model="form.Customer.CustomerId" placeholder="">
                        <el-option :label="customer.Customer_name" :value="customer.CustomerId" v-for='customer in customers'>
                        </el-option>
                    </el-select>
                </el-form-item>
                <el-form-item label="Type">
                    <el-select v-model="form.Type" placeholder="">
                        <el-option label="P" value="P"></el-option>
                        <el-option label="O" value="O"></el-option>
                        <el-option label="R" value="R"></el-option>
                    </el-select>
                </el-form-item>
                <el-form-item label="Status">
                    <el-select v-model="form.Status" placeholder="">
                        <el-option label="Scheduled" value="Scheduled"></el-option>
                        <el-option label="Ongoning" value="Ongoning"></el-option>
                        <el-option label="Completed" value="Completed"></el-option>
                        <el-option label="Cancelled" value="Cancelled"></el-option>
                    </el-select>
                </el-form-item>
                <el-form-item label="Select Time Span:">
                    <el-date-picker v-model="timeSpan" type="daterange" range-separator="To" start-placeholder="Start"
                        end-placeholder="End" @change='changeTimeSpan'>
                    </el-date-picker>
                </el-form-item>
            </el-form>
            <el-button style="margin-top: 12px;" @click="basicInfoSave">Save and Next</el-button>
        </div>


        <div v-show="active==1">
                <span class="demonstration" style='font-weight:bold'>Select Consultants: </span>
                <el-checkbox-group v-model="checkList" style="display:inline-block">
                    <el-checkbox :label="employee.Consultant_Name" v-for='employee in consultants'></el-checkbox>
                </el-checkbox-group>
                <span v-if='consultants.length<=0'>
                    Please Add Employee First
                </span>
                <el-button type="primary" @click='addEmployee'>Add Emlpoyee</el-button>

                <el-table border :data="infiledList" style="width: 100%">
                    <el-table-column prop="fildna" label="Name" style="width:6vw;">
                        <template scope="scope">
                            <el-input size="mini" v-model="scope.row.Consultant_Name" disabled="disabled">
                            </el-input>
                        </template>
                    </el-table-column>
                    <!-- <el-table-column prop="Month" label="Month" style="width:6vw;">
                        <template scope="scope">
                            <el-input size="mini" v-model="scope.row.Month" disabled="disabled"></el-input>
                        </template>
                    </el-table-column> -->
                    <el-table-column prop="Type" label="Type">
                        <template scope="scope">
                            <el-input size="mini" v-model="scope.row.Type" disabled="disabled"></el-input>
                        </template>
                    </el-table-column>
                    <el-table-column prop="CostRate" label="CostRate">
                        <template scope="scope">
                            <el-input size="mini" v-model="scope.row.CostRate" disabled="disabled"></el-input>
                        </template>
                    </el-table-column>
                    <el-table-column prop="item" :label="item" v-for="(item,i) in columnShow">
                        <template scope="scope">
                            <el-input size="mini" v-model="scope.row.WorkingMonth[item]"></el-input>
                        </template>
                    </el-table-column>
                    <!-- <el-table-column prop="WorkDays" label="WorkDays">
                        <template scope="scope">
                            <el-input size="mini" v-model="scope.row.WorkDays"></el-input>
                        </template>
                    </el-table-column> -->
                    <el-table-column fixed="right" label="Operation">
                        <template slot-scope="scope">
                            <el-button @click.native.prevent="deleteRow(scope.$index, infiledList,scope.row.Id)"
                                size="small"> Remove
                            </el-button>
                        </template>
                    </el-table-column>
                </el-table>

                <el-button style="margin-top: 12px;" @click="SaveEmployeeUtilization">Save and Next</el-button>
        </div>

        <div class="block" v-show="active==2">

                <el-table border :data="sumList" style="width: 100%">
                    <el-table-column prop="Month" label="Month" style="width:6vw;">
                        <template scope="scope">
                            <el-input size="mini" v-model="scope.row.Month" disabled="disabled"></el-input>
                        </template>
                    </el-table-column>
                    <el-table-column prop="HeadCountCost" label="HeadCountCost" style="width:6vw;">
                        <template scope="scope">
                            <el-input size="mini" v-model="scope.row.HeadCountCost" disabled="disabled"></el-input>
                        </template>
                    </el-table-column>
                    <el-table-column prop="ChargesIn" label="ChargesIn" style="width:6vw;">
                        <template scope="scope">
                            <el-input size="mini" v-model="scope.row.ChargesIn" disabled="disabled"></el-input>
                        </template>
                    </el-table-column>
                    <el-table-column prop="Contractors" label="Contractors" style="width:6vw;">
                        <template scope="scope">
                            <el-input size="mini" v-model="scope.row.Contractors" disabled="disabled"></el-input>
                        </template>
                    </el-table-column>
                    <el-table-column prop="GP" label="GP" style="width:6vw;">
                        <template scope="scope">
                            <el-input size="mini" v-model="scope.row.GP"></el-input>
                        </template>
                    </el-table-column>
                    <el-table-column prop="Revenue" label="Revenue" style="width:6vw;">
                        <template scope="scope">
                            <el-input size="mini" v-model="scope.row.Revenue"></el-input>
                        </template>
                    </el-table-column>
                    <el-table-column prop="Expenses" label="Expenses">
                        <template scope="scope">
                            <el-input size="mini" v-model="scope.row.Expenses"></el-input>
                        </template>
                    </el-table-column>
                    <el-table-column prop="CostRate" label="IT(HW/SW)">
                        <template scope="scope">
                            <el-input size="mini" v-model="scope.row.IT"></el-input>
                        </template>
                    </el-table-column>
                    <el-table-column prop="Materials" label="Materials">
                        <template scope="scope">
                            <el-input size="mini" v-model="scope.row.Materials"></el-input>
                        </template>
                    </el-table-column>

                    <el-table-column fixed="right" label="操作">
                        <template slot-scope="scope">
                            <el-button @click.native.prevent="deleteRow(scope.$index, infiledList)" size="small"> 移除
                            </el-button>
                        </template>
                    </el-table-column>
                </el-table>
                <el-button style="margin-top: 12px;" @click="Save">Save</el-button>

        </div>

           
        </div>

    </div>
</template>
<script>
    var formData_service = require('./../../services/form-data-service');
    var moment = require('moment');
    import axios from "axios";
    import qs from 'qs';
    export default {
        data() {
            return {
                form: { Consultant: { Consultant_Id: '' }, Country: { CountryId: '' }, Customer: { CustomerId: '' } },
                CloseDate: '',
                checkList: [],
                customers: [],
                consultants: [],
                Country: [],
                timeSpan: '',
                infiledList: [],
                FinancialReport: [],
                isAdded: false,
                startMonth: '',
                endMonth: '',
                sumList: [],
                active:0,
                word:'Next',
                columnShow:[]
            }
        },
        created() {
            formData_service.default.getAllEmployee.extc()
                .then(data => {
                    this.consultants = data.data;
                });
            formData_service.default.getAllCountry.extc()
                .then(data => {
                    this.Country = data.data;
                });
            formData_service.default.getAllCustomer.exec()
                .then((data) => {
                    this.customers = data.data;
                })
        },
        computed: {

        },
        watch: {
        },
        methods: {
            MonthMapping(month) {
                switch (month) {
                    case 1: return "Mon";
                    case 2: return "Feb";
                    case 3: return "Mar";
                    case 4: return "Apr";
                    case 5: return "May";
                    case 6: return "Jun";
                    case 7: return "Jul";
                    case 8: return "Aug";
                    case 9: return "Sep";
                    case 10: return "Oct";
                    case 11: return "Nov";
                    case 12: return "Dec";
                }
            },
            deleteRow(index, rows) {//删除改行
                rows.splice(index, 1);
            },
            addCountry() {
                this.$prompt('Please input the country name', '', {
                    confirmButtonText: 'Add',
                    cancelButtonText: 'Cancle',
                }).then(({ value }) => {
                    formData_service.default.addCountry.extc({ CountryName: value });
                    this.Country.push(value);
                }).catch((err) => {
                    console.log(err);
                    this.$message({
                        type: 'info',
                        message: 'Cancled'
                    });
                })
            },

            changeTimeSpan() {
                var _vm = this;
                _vm.form.StartDate=moment(this.timeSpan[0]).format('MM-DD-YYYY');
                _vm.form.CloseDate=moment(this.timeSpan[1]).format('MM-DD-YYYY');
                _vm.startMonth = parseInt(moment(this.timeSpan[0]).format('MM-DD-YYYY').split('-')[0]);
                _vm.endMonth = parseInt(moment(this.timeSpan[1]).format('MM-DD-YYYY').split('-')[0]);
            },
            
            addEmployee() {
                var _vm = this;

                this.checkList.forEach(element => {
                    let consultant = _vm.getProperty(this.consultants, 'Consultant_Name', element);
                    let workdays = {};
                    let exists = false;
                    for (let i = _vm.startMonth; i <= _vm.endMonth; i++) {
                        let month = _vm.MonthMapping(i);
                        workdays[month] = 0;
                    }

                    _vm.infiledList.forEach(utilization => {
                        if (utilization.Consultant_Name == element) {
                            exists = true;
                        }
                    })

                    if (!exists) {
                        this.infiledList.push({
                            Consultant_Id: consultant.Consultant_Id, Consultant_Name: consultant.Consultant_Name, Type: consultant.Type,
                            CostRate: consultant.CostRate, WorkingMonth: { ...workdays }
                        })
                    }
                });
            },
            getProperty(list, property, value) {
                for (let i in list) {
                    if (list[i][property] == value) {
                        return list[i];
                    }
                }
            },

            groupBy(array, f) {
                let groups = {};
                array.forEach(function (o) {
                    let group = JSON.stringify(f(o));
                    groups[group] = groups[group] || [];
                    groups[group].push(o);
                });
                return Object.keys(groups).map(function (group) {
                    return groups[group];
                });
            },

            Save() {
                var _vm = this;
                if (this.active <= 2) {
                    if (this.active == 2) {

                        let project = this.form;
                        project.StartDate = moment(project.StartDate).format('MM-DD-YYYY');
                        project.CloseDate = moment(project.CloseDate).format('MM-DD-YYYY');

                        project.ProjectFinancList = _vm.sumList;
                        for (let j in project.ProjectFinancList) {
                            //已存在，编辑
                            if (!project.ProjectFinancList[j].Id) {
                                project.ProjectFinancList[j].ProjectNo = _vm.form.ProjectNo;
                            }
                        }

                        formData_service.default.addProjectFinance.extc(_vm.sumList)
                    }
                    this.active++;
                    this.$router.push({
                        name: 'project_list',
                    })
                }
            },
            basicInfoSave() {
                var _vm = this;
                for (let i = _vm.startMonth; i <= _vm.endMonth; i++) {
                    let month = _vm.MonthMapping(i);
                    _vm.columnShow.push(month);
                }
                this.active++;
            },
            SaveEmployeeUtilization(){
                var _vm = this;
              
                let project = _vm.form;

                project.Employees = _vm.infiledList;

                formData_service.default.addProject.extc(project).then(function (res) {
                    if (res.data != "") {
                        formData_service.default.getProjectFinance.exec(project.ProjectNo)
                            .then(data => {
                                console.log(data);

                                _vm.sumList = data.data;
                                _vm.active = 2;
                            })
                    }
                    else {
                        this.$message('The Project is already exists');
                        this.$router.push({
          name: 'project_add',
        })
                    }
                }).catch(err => {
                    this.$message(err.status);
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