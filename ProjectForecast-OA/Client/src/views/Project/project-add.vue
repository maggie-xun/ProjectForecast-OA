<template>
    <div>
        <div class="side-nav">
            <el-tabs v-model="activeName" type="border-card" style='line-height: 40px;'>
                <el-tab-pane label="Basic Info" name="first" style='line-height: 20px;'>
                    <el-form ref="form" :model="form" label-width="120px">
                        <el-form-item label="Country">
                            <el-select v-model="form.Country.CountryId" placeholder="Please Select a Country">
                                <el-option :label="country.CountryName" :value="country.CountryId" v-for='country in Country'></el-option>
                            </el-select>
                            <el-button type="text" @click="open">Add Country</el-button>
                        </el-form-item>
                        <el-form-item label="Project Number">
                            <el-input v-model="form.ProjectNo"></el-input>
                        </el-form-item>
                        <el-form-item label="Project Name">
                            <el-input v-model="form.ProjectName"></el-input>
                        </el-form-item>
                        <el-form-item label="Project Manager" label-width="121px">
                                <el-select v-model="form.Consultant.Consultant_ID" placeholder="">
                                    <el-option :label="consultant.Consultant_Name" :value="consultant.Consultant_ID"
                                        v-for='consultant in consultants'></el-option>
                                </el-select>
                            </el-form-item>
                        <el-form-item label="Customer">
                                <el-select v-model="form.Customer.CustomerId" placeholder="">
                                    <el-option :label="customer.Customer_name" :value="customer.CustomerId" v-for='customer in customers'></el-option>
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
                        <el-form-item label="StartDate">
                                <div class="block">
                                    <el-date-picker v-model="form.StartDate" type="date" placeholder="">
                                    </el-date-picker>
                                </div>
                            </el-form-item>
                        <el-form-item label="CloseDate">
                            <div class="block">
                                <el-date-picker v-model="form.CloseDate" type="date" placeholder="">
                                </el-date-picker>
                            </div>
                        </el-form-item>
                        <el-form-item>
                            <el-button type="primary">Add Now</el-button>
                        </el-form-item>
                    </el-form>
                </el-tab-pane>
                <el-tab-pane label="Utilization" name="second">
                    <span class="demonstration">Select Consultants: </span>
                    <el-checkbox-group v-model="checkList" style="display:inline-block">
                        <el-checkbox :label="employee.Consultant_Name" v-for='employee in consultants'></el-checkbox>
                    </el-checkbox-group>
                    <span v-if='consultants.length<=0'>
                        Please Add Employee First
                    </span>

                    <div class="block">
                        <span class="demonstration" style="display:inline-block">Select Time Span: </span>
                        <el-date-picker v-model="timeSpan" type="daterange" range-separator="To"
                            start-placeholder="Start" end-placeholder="End" @change='changeUtilizationTimeSpan'>
                        </el-date-picker>
                    </div>
                    <el-table border :data="infiledList" style="width: 100%">
                            <el-table-column prop="fildna" label="Name" style="width:6vw;">
                                <template scope="scope">
                                    <el-input size="mini" v-model="scope.row.Consultant_Name" disabled="disabled">
                                    </el-input>
                                </template>
                            </el-table-column>
                            <el-table-column prop="Month" label="Month" style="width:6vw;">
                                <template scope="scope">
                                    <el-input size="mini" v-model="scope.row.Month" disabled="disabled"></el-input>
                                </template>
                            </el-table-column>
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
                            <el-table-column prop="CostRate" label="WorkDays">
                                <template scope="scope">
                                    <el-input size="mini" v-model="scope.row.WorkDays"></el-input>
                                </template>
                            </el-table-column>
                            <el-table-column fixed="right" label="Operation">
                                <template slot-scope="scope">
                                    <el-button @click.native.prevent="deleteRow(scope.$index, infiledList,scope.row.Id)"
                                        size="small"> Remove
                                    </el-button>
                                </template>
                            </el-table-column>
                        </el-table>
                    <el-button type="primary">Save</el-button>
                </el-tab-pane>
                <el-tab-pane label="FinancialReport" name="third">
                    <div class="block">
                        <span class="demonstration">Please Select Time Span</span>
                        <el-date-picker v-model="FinancialReportTimeSpan" type="daterange" range-separator="To" start-placeholder="Start" @change='changeFinancialReportTimeSpan'
                            end-placeholder="Ends">
                        </el-date-picker>

                        <el-table border :data="FinancialReport" style="width: 100%">
                            <el-table-column prop="Month" label="Month" style="width:6vw;">
                                <template scope="scope">
                                    <el-input size="mini" v-model="scope.row.Month" disabled="disabled"></el-input>
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

                        <el-button type="primary" @click='AddProject'>Add Project</el-button>
                    </div>
                </el-tab-pane>
            </el-tabs>
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
                form: { Consultant:{Consultant_ID:''} ,Country:{CountryId:''},Customer:{CustomerId:''}},
                CloseDate: '',
                activeName: 'first',
                checkList: [],
                customers:[],
                consultants:[],
                timeSpan: '',
                FinancialReportTimeSpan: [],
                infiledList: [],
                months: [],
                FinancialReportMonths: [],
                FinancialReport: [],
                Country: [],
                conntry_added: "",
                Consultant:{},
                Customer:{},
                isAdded: false
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
                .then((data)=>{
                    this.customers=data.data;
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
            onSubmit() {
                console.log(formData_service);
                formData_service.default.upload.extc(this.form)
            },
            deleteRow(index, rows) {//删除改行
                rows.splice(index, 1);
            },
            addRow(tableData, event) {
                tableData.push({ fildna: '', fildtp: '', remark: '' })
            },
            open() {
                this.$prompt('Please input the country name', '', {
                    confirmButtonText: 'Add',
                    cancelButtonText: 'Cancle',
                }).then(({ value }) => {
                    formData_service.default.addCountry.extc({ CountryName: value })
                }).then((data) => {
                   this.Country.push("");
                   
                }).catch((err) => {
                    console.log(err);
                    this.$message({
                        type: 'info',
                        message: 'Cancled'
                    });
                })
            },
            
            AddProject() {
                var _vm = this;
                let project = this.form;
                project.CloseDate = moment(project.CloseDate).format('MM-DD-YYYY');
                project.StartDate = moment(project.StartDate).format('MM-DD-YYYY');
                project.Country = _vm.form.Country;
                project.Consultant = _vm.form.Consultant;
                project.Customer = _vm.form.Customer;
                project.Employees=_vm.infiledList  ;
               
                
                for (let j in project.Employees) {
                        if (!project.Employees[j].Id) {
                            project.Employees[j].ProjectNo = _vm.form.ProjectNo;
                        }
                    }
                    project.ProjectFinancList=_vm.FinancialReport;
                    for (let j in project.ProjectFinancList) {
                            //已存在，编辑
                            if (!project.ProjectFinancList[j].Id) {
                                project.ProjectFinancList[j].ProjectNo = _vm.form.ProjectNo;
                            }
                        }

                formData_service.default.addProject.extc(project)

                // project.Employees=infiledList;
                // project.ProjectFinancList=this.FinancialReport;

            },
            changeFinancialReportTimeSpan() {
                let startMonth = parseInt(moment(this.FinancialReportTimeSpan[0]).format('MM-DD-YYYY').split('-')[0]);

                let endMonth = parseInt(moment(this.FinancialReportTimeSpan[1]).format('MM-DD-YYYY').split('-')[0]);
                for (let i = startMonth; i < endMonth + 1; i++) {
                    this.FinancialReport.push({ Month: i + '' });
                }
            },
            changeUtilizationTimeSpan() {
                    var _vm = this;
                    let startMonth = parseInt(moment(this.timeSpan[0]).format('MM-DD-YYYY').split('-')[0]);

                    let endMonth = parseInt(moment(this.timeSpan[1]).format('MM-DD-YYYY').split('-')[0]);
                    for (let i = startMonth; i <= endMonth; i++) {
                        this.checkList.forEach(element => {
                        this.consultants.forEach(employee => {
                            if (employee.Consultant_Name == element) {
                                this.infiledList.push({
                                    Consultant_Id: employee.Consultant_Id, Consultant_Name: employee.Consultant_Name, Type: employee.Type, CostRate: employee.CostRate, Month: _vm.MonthMapping(i)
                                })
                            }
                        })

                    });
                    }                   
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