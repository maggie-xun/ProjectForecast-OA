<template>
    <div>
        <div class="side-nav">
            <el-tabs v-model="activeName" type="border-card" style='line-height: 40px;'>
                <el-tab-pane label="Basic Info" name="first" style='line-height: 20px;'>
                    <el-form ref="form" :model="form" label-width="120px">
                        <el-form-item label="Country">
                            <el-select v-model="form.Country.CountryId" placeholder="Please Select a Country">
                                <el-option :label="country.CountryName" :value="country.CountryId"
                                    v-for='country in Countries'></el-option>
                            </el-select>
                            <el-button type="text" @click="open">Add Country</el-button>
                        </el-form-item>
                        <el-form-item label="Project Number">
                            <el-input v-model="form.ProjectNo"></el-input>
                        </el-form-item>
                        <el-form-item label="Project Name">
                            <el-input v-model="form.ProjectName"></el-input>
                        </el-form-item>
                        <el-form-item label="Project Manager">
                            <el-select v-model="form.Consultant.Consultant_ID" placeholder="">
                                <el-option :label="consultant.Consultant_Name" :value="consultant.Consultant_ID"
                                    v-for='consultant in consultants'></el-option>
                            </el-select>
                        </el-form-item>
                        <el-form-item label="Customer">
                            <el-select v-model="form.Customer.CustomerId" placeholder="">
                                <el-option :label="customer.Customer_name" :value="customer.CustomerId"
                                    v-for='customer in customers'></el-option>
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
                            <el-button type="primary">Save Now</el-button>
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
                        <el-table-column prop="MonthElement" :label="MonthMapping(month)" v-for='(month,index) in months'>
                            <template scope="scope">
                                <el-input size="mini" v-model="scope.row.MonthElement[index].WorkingHour"></el-input>
                            </template>
                        </el-table-column>
                        <el-table-column fixed="right" label="Operation">
                            <template slot-scope="scope">
                                <el-button @click.native.prevent="deleteRow(scope.$index, infiledList)" size="small"> Remove
                                </el-button>
                            </template>
                        </el-table-column>
                    </el-table>
                    <el-button type="primary">Save</el-button>
                </el-tab-pane>
                <el-tab-pane label="FinancialReport" name="third">
                    <div class="block">
                        <span class="demonstration">Please Select Time Span</span>
                        <el-date-picker v-model="FinancialReportTimeSpan" type="daterange" range-separator="To"
                            start-placeholder="Start" @change='changeFinancialReportTimeSpan' end-placeholder="Ends">
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

                            <el-table-column fixed="right" label="Operation">
                                <template slot-scope="scope">
                                    <el-button @click.native.prevent="deleteRow(scope.$index, infiledList)"
                                        size="small"> Remove
                                    </el-button>
                                </template>
                            </el-table-column>
                        </el-table>

                        <el-button type="primary" @click='EditProject'>Save Project</el-button>
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
                consultants: [],
                customers: [],
                timeSpan: '',
                FinancialReportTimeSpan: [],
                infiledList: [],
                months: [],

                FinancialReportMonths: [],
                FinancialReport: [],
                Countries: [],
                conntry_added: "",
                isAdded: false
            }
        },
        created() {
            var _vm = this;
            formData_service.default.getAllEmployee.extc()
                .then(data => {
                    this.consultants = data.data;
                });
            formData_service.default.getAllCountry.extc()
                .then(data => {
                    this.Countries = data.data;
                });

            formData_service.default.getAllCustomer.exec()
                .then((data) => {
                    this.customers = data.data;
                })

            _vm.projectId = this.$route.params.item;
            formData_service.default.getProjectPerId.exec(_vm.projectId)
                .then(data => {
                    if (data.data.length > 0) {
                        _vm.detailData = data.data[0];
                        _vm.form = _vm.detailData;
                        if (!_vm.form.Country) {
                            _vm.form.Country = { CountryId: '' }
                        }
                        // _vm.infiledList = _vm.detailData.Employees;
                        let result = _vm.groupBy(_vm.detailData.Employees, function (item) {
                            return [item.Consultant_Id];
                        })
                        console.log(result);
                        result.forEach(employee => {
                            let consultant_show = { Id: employee[0].Id, Consultant_Id: employee[0].Consultant_Id, Consultant_Name: employee[0].Consultant_Name, Type: employee[0].Type, CostRate: employee[0].CostRate, ProjectNo: this.form.ProjectNo, Month: employee[0].Month, };
                            let MonthElement = [];
                            employee.forEach(monthElement => {
                                let exits = false;
                                for (let j in _vm.months)
                                // _vm.months.forEach(element => {
                                {
                                    if (_vm.months[j] == _vm.MonthMappingReverse(monthElement.Month)) {
                                        exits = true;
                                    }
                                }
                                // });
                                if (!exits) {
                                    _vm.months.push(_vm.MonthMappingReverse(monthElement.Month));
                                }

                                MonthElement.push({ Month: monthElement.Month, WorkingHour: monthElement.WorkDays })

                            });
                            consultant_show.MonthElement = MonthElement;
                            _vm.infiledList.push(consultant_show);
                        });
                        _vm.months.sort();
                        _vm.FinancialReport = _vm.detailData.ProjectFinancList;
                        _vm.loading = false;
                    }

                    console.log(_vm.form);
                });

        },
        computed: {

        },
        watch: {

        },
        methods: {
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
            MonthMapping(month) {
                switch (month) {
                    case "1": return "Mon";
                    case "2": return "Feb";
                    case "3": return "Mar";
                    case "4": return "Apr";
                    case "5": return "May";
                    case "6": return "Jun";
                    case 7: return "Jul";
                    case "8": return "Aug";
                    case "9": return "Sep";
                    case "10": return "Oct";
                    case "11": return "Nov";
                    case "12": return "Dec";
                }
            },
            MonthMappingReverse(month) {
                switch (month) {
                    case "Mon": return 1;
                    case "Feb": return 2;
                    case "Mar": return 3;
                    case "Apr": return 4;
                    case "May": return 5;
                    case "Jun": return 6;
                    case "Jul": return 7;
                    case "Aug": return 8;
                    case "9": return 9;
                    case "Oct": return 10;
                    case "Nov": return 11;
                    case "Dec": return 12;
                }
            },
            NextPage() {
                this.activeName = 'second';
            },
            deleteRow(index, rows) {//删除改行
                rows.splice(index, 1);
            },
            onSubmit() {
                console.log(formData_service);
                formData_service.default.upload.extc(this.form)
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

            EditProject() {
                var _vm = this;
                let project = this.form;
                project.CloseDate = moment(project.CloseDate).format('MM-DD-YYYY');
                project.StartDate = moment(project.StartDate).format('MM-DD-YYYY');
                project.Country = _vm.form.Country;
                if (project.ProjectFinancList.length <= 0) {
                    project.ProjectFinancList = [];
                }
                if (project.Employees.length <= 0) {
                    project.Employees = [];
                }
                for (let i in _vm.infiledList) {
                    if (_vm.months.length > 0) {
                        _vm.months.forEach(month => {
                            _vm.infiledList[i].MonthElement.forEach(monthElement => {
                                let exist = false;
                                //修改
                                for (let j in project.Employees) {
                                    if (_vm.infiledList[i].Id == project.Employees[j].Id) {
                                        exist = true;
                                        _vm.infiledList[i].WorkDays = monthElement.WorkingHour;
                                    }
                                }
                                //新添加的
                                if (!exist) {
                                    _vm.infiledList.push({ Id: _vm.infiledList[i].Id, Consultant_Id: _vm.infiledList[i].Consultant_Id, Consultant_Name: _vm.infiledList[i].Consultant_Name, Type: _vm.infiledList[i].Type, CostRate: _vm.infiledList[i].CostRate, ProjectNo: _vm.form.ProjectNo, Month: _vm.MonthMapping(month), WorkDays: monthElement.WorkingHour })
                                    _vm.infiledList.splice(i, 1);
                                }
                            })
                        })

                    }
                }
                project.Employees = _vm.infiledList;
                for (let i in _vm.FinancialReport) {
                    if (this.FinancialReportMonths.length > 0) {
                        let exist = false;
                        this.FinancialReportMonths.forEach(month => {
                            for (let j in project.ProjectFinancList) {
                                if (_vm.FinancialReport[i].Id) {
                                    if (project.ProjectFinancList[j].Id == _vm.FinancialReport[i].Id) {
                                        exist = true;
                                        project.ProjectFinancList[j] = _vm.FinancialReport[i];
                                    }
                                }
                            };
                            if (!exist) {
                                //跨年问题需处理
                                let financeMonth = month;
                                let financeYear = parseInt(moment(this.FinancialReportTimeSpan[1]).format('MM-DD-YYYY').split('-')[2])//parseInt(moment(month).format('MM-DD-YYYY').split('-')[2]);
                                project.ProjectFinancList.push({
                                    ProjectNo: this.form.ProjectNo, Revenue: _vm.FinancialReport[i].Revenue, Expenses: _vm.FinancialReport[i].Expenses,
                                    IT: _vm.FinancialReport[i].IT, Materials: _vm.FinancialReport[i].Materials, Month: financeMonth, Year: financeYear
                                });
                                project.ProjectFinancList.splice(i, 1);

                            }
                        })
                    }
                    else {
                        project.ProjectFinancList.forEach(projectFinance => {
                            if (element.Id == projectFinance.Id) {
                                projectFinance = element;
                            }
                        })
                    }
                };
                formData_service.default.editProject.extc(project)
            },
            changeFinancialReportTimeSpan() {
                var _vm = this;
                let startMonth = parseInt(moment(this.FinancialReportTimeSpan[0]).format('MM-DD-YYYY').split('-')[0]);
                let endMonth = parseInt(moment(this.FinancialReportTimeSpan[1]).format('MM-DD-YYYY').split('-')[0]);
                for (let i = startMonth; i <= endMonth; i++) {
                    let exits = false;
                    for (let j in _vm.FinancialReportMonths) {
                        if (_vm.FinancialReportMonths[j] == i) {
                            exits = true;
                        }
                    }
                    if (!exits) {
                        this.FinancialReportMonths.push(i);
                    }
                    // this.FinancialReport.push({ Month: i + '' });
                }
            },
            changeUtilizationTimeSpan() {
                var _vm = this;
                let startMonth = parseInt(moment(this.timeSpan[0]).format('MM-DD-YYYY').split('-')[0]);
                let endMonth = parseInt(moment(this.timeSpan[1]).format('MM-DD-YYYY').split('-')[0]);
                for (let i = startMonth; i <= endMonth; i++) {
                    let exits = false;
                    for (let j in _vm.months) {
                        if (_vm.months[j] == i) {
                            exits = true;
                        }
                    }
                    if (!exits) {
                        this.months.push(i);
                    }

                }

                this.checkList.forEach(element => {
                    this.consultants.forEach(employee => {
                        if (employee.Consultant_Name == element) {
                            let MonthElement = []
                            this.months.forEach(month => {
                                MonthElement.push({ Month: month, WorkingHour: '' })
                            })
                            this.infiledList.push({
                                Consultant_Id: employee.Consultant_Id, Consultant_Name: employee.Consultant_Name, Type: employee.Type, CostRate: employee.CostRate, MonthElement: MonthElement
                            })
                        }
                    })

                });
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