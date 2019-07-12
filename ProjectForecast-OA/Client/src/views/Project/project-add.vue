<template>
    <div>
        <div class="side-nav">
            <el-tabs v-model="activeName" type="border-card" style='line-height: 40px;'>
                <el-tab-pane label="Basic Info" name="first" style='line-height: 20px;'>
                    <el-form ref="form" :model="form" label-width="120px">
                        <el-form-item label="Country">
                            <el-select v-model="form.Country" placeholder="Please Select a Country">
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
                        <el-form-item label="Customer">
                            <el-input v-model="form.Customer"></el-input>
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
                            <el-button type="primary">Add Now</el-button>
                            <el-button type="primary" @click='NextPage'>Next Tab</el-button>
                        </el-form-item>
                    </el-form>
                </el-tab-pane>
                <el-tab-pane label="Utilization" name="second">
                    <span class="demonstration">Select Consultants: </span>
                    <el-checkbox-group v-model="checkList" style="display:inline-block">
                        <el-checkbox :label="employee.Name" v-for='employee in allEmployees'></el-checkbox>
                    </el-checkbox-group>
                    <span v-if='allEmployees.length<=0'>
                        Please Add Employee First
                    </span>

                    <div class="block">
                        <span class="demonstration" style="display:inline-block">Select Time Span: </span>
                        <el-date-picker v-model="timeSpan" type="daterange" range-separator="To"
                            start-placeholder="Start" end-placeholder="End">
                        </el-date-picker>
                        <el-button type="primary" @click="AddEmployee">AddEmployee</el-button>
                    </div>
                    <el-table border :data="infiledList" style="width: 100%">
                        <el-table-column prop="fildna" label="Name" style="width:6vw;">
                            <template scope="scope">
                                <el-input size="mini" v-model="scope.row.Name" disabled="disabled"></el-input>
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
                        <el-table-column prop="MonthElement" :label="month" v-for='(month,index) in months'>
                            <template scope="scope">
                                <el-input size="mini" v-model="scope.row.MonthElement[index].WorkingHour"></el-input>
                            </template>
                        </el-table-column>
                        <el-table-column fixed="right" label="操作">
                            <template slot-scope="scope">
                                <el-button @click.native.prevent="deleteRow(scope.$index, infiledList)" size="small"> 移除
                                </el-button>
                            </template>
                        </el-table-column>
                    </el-table>
                    <el-button type="primary">Save</el-button>
                    <el-button type="primary">Next Tab</el-button>
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
                form: { CloseDate: '' },
                CloseDate: '',
                activeName: 'first',
                checkList: [],
                allEmployees: [],
                timeSpan: '',
                FinancialReportTimeSpan: [],
                infiledList: [],
                months: [],
                FinancialReportMonths: [],
                FinancialReport: [],
                Country: [],
                conntry_added: "",
                isAdded: false
            }
        },
        created() {
            formData_service.default.getAllEmployee.extc()
                .then(data => {
                    this.allEmployees = data.data;
                });
            formData_service.default.getAllCountry.extc()
                .then(data => {
                    this.Country = data.data;
                });

        },
        computed: {

        },
        watch: {

        },
        methods: {
            NextPage(){
                this.activeName='second';
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
            AddEmployee() {
                let startMonth = parseInt(moment(this.timeSpan[0]).format('MM-DD-YYYY').split('-')[0]);

                let endMonth = parseInt(moment(this.timeSpan[1]).format('MM-DD-YYYY').split('-')[0]);
                for (let i = startMonth; i < endMonth + 1; i++) {
                    this.months.push(i + '');
                }

                this.checkList.forEach(element => {
                    this.allEmployees.forEach(employee => {
                        if (employee.Name == element) {
                            let MonthElement = []
                            this.months.forEach(month => {
                                MonthElement.push({ Month: month, WorkingHour: '' })

                            })
                            this.infiledList.push({
                                EmployeeId: employee.EmployeeId,
                                Name: employee.Name, Type: employee.Type, CostRate: employee.CostRate, MonthElement: MonthElement
                            })
                        }
                    })

                });
            },
            open() {
                this.$prompt('Please input the country name', '', {
                    confirmButtonText: 'Add',
                    cancelButtonText: 'Cancle',
                }).then(({ value }) => {
                    formData_service.default.addCountry.extc({ CountryName: value })
                }).catch(() => {
                    this.$message({
                        type: 'info',
                        message: 'Cancled'
                    });
                });
            },

            AddProject() {
                let project = this.form;
                project.CloseDate = moment(project.CloseDate).format('MM-DD-YYYY');
                project.Employees = [];
                project.ProjectFinancList = [];
                this.infiledList.forEach(element => {
                    this.months.forEach(month => {
                        let workingHour;
                        element.MonthElement.forEach(monthElement => {
                            if (monthElement.Month == month) {
                                workingHour = monthElement.WorkingHour;
                            }
                        })

                        project.Employees.push({ EmployeeId: element.EmployeeId, ProjectNo: this.form.ProjectNo, Month: month, WorkingHour: workingHour })
                    })

                });
                this.FinancialReport.forEach(element => {
                    this.FinancialReportTimeSpan.forEach(month => {
                        let financeMonth = parseInt(moment(month).format('MM-DD-YYYY').split('-')[0]);
                        let financeYear=parseInt(moment(month).format('MM-DD-YYYY').split('-')[2]);
                        project.ProjectFinancList.push({
                            ProjectNo: this.form.ProjectNo, Revenue: element.Revenue, Expenses: element.Expenses,
                            IT: element.IT, Materials: element.Materials, Month: financeMonth,Year:financeYear
                        })
                    })

                });
                formData_service.default.addProject.extc(this.form)

                // project.Employees=infiledList;
                // project.ProjectFinancList=this.FinancialReport;

            },
            changeFinancialReportTimeSpan() {
                let startMonth = parseInt(moment(this.FinancialReportTimeSpan[0]).format('MM-DD-YYYY').split('-')[0]);

                let endMonth = parseInt(moment(this.FinancialReportTimeSpan[1]).format('MM-DD-YYYY').split('-')[0]);
                for (let i = startMonth; i < endMonth + 1; i++) {
                    this.FinancialReport.push({ Month: i + '' });
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