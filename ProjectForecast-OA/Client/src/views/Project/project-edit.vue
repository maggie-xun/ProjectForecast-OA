<template>
    <div>
        <div class="side-nav">
            <el-steps :active="active" finish-status="success">
                <el-step title="Basic Info"></el-step>
                <el-step title="Utilization"></el-step>
                <el-step title="FinancialReport"></el-step>
            </el-steps>

            <el-form ref="form" :model="form" label-width="120px" v-show="active==0">
                <el-form-item label="Country">
                    <el-select v-model="form.Country.CountryId" placeholder="Please Select a Country">
                        <el-option :label="country.CountryName" :value="country.CountryId" v-for='country in Countries'>
                        </el-option>
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
                    <el-select v-model="form.Consultant.Consultant_Id" placeholder="">
                        <el-option :label="consultant.Consultant_Name" :value="consultant.Consultant_Id"
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
                <el-form-item label="Select Time Span:">
                    <el-date-picker v-model="timeSpan" type="daterange" range-separator="To" start-placeholder="Start"
                        end-placeholder="End" @change='changeTimeSpan'>
                    </el-date-picker>
                </el-form-item>
            </el-form>

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
                                <el-input size="mini" v-model="scope.row.HeadCountCost"></el-input>
                            </template>
                        </el-table-column>
                        <el-table-column prop="ChargesIn" label="ChargesIn" style="width:6vw;">
                            <template scope="scope">
                                <el-input size="mini" v-model="scope.row.ChargesIn"></el-input>
                            </template>
                        </el-table-column>
                        <el-table-column prop="Contractors" label="Contractors" style="width:6vw;">
                            <template scope="scope">
                                <el-input size="mini" v-model="scope.row.Contractors"></el-input>
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

                    <el-table-column fixed="right" label="Operation">
                        <template slot-scope="scope">
                            <el-button
                                @click.native.prevent="deleteFinanceRow(scope.$index, FinancialReport,scope.row.Id)"
                                size="small"> Remove
                            </el-button>
                        </template>
                    </el-table-column>
                </el-table>
            </div>

            <el-button style="margin-top: 12px;" @click="next">{{word}}</el-button>
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
                form: { CloseDate: '', Consultant: { Consultant_Id: '' }, Country: { CountryId: '' }, Customer: { CustomerId: '' } },
                CloseDate: '',
                activeName: 'first',
                checkList: [],
                consultants: [],
                customers: [],
                timeSpan: '',
                infiledList: [],
                FinancialReport: [],
                Countries: [],
                conntry_added: "",
                isAdded: false,
                loading: false,
                sumList: [],
                active: 0,
                word:'Next'
            }
        },
        created() {
            var _vm = this;
            _vm.loading = true;
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

            formData_service.default.getProjectPerId.exec(this.$route.params.item)
                .then(data => {
                    if (data.data.length > 0) {
                        _vm.detailData = data.data[0];
                        _vm.form = _vm.detailData;
                        if (!_vm.form.Country) {
                            _vm.form.Country = { CountryId: '' }
                        }
                        if (!_vm.form.Consultant) {
                            _vm.form.Consultant = { Consultant_Id: '' }
                        }
                        _vm.infiledList = _vm.detailData.Employees;
                        _vm.timeSpan=[_vm.form.StartDate,_vm.form.CloseDate];
                        _vm.startMonth = parseInt(moment(this.timeSpan[0]).format('MM-DD-YYYY').split('-')[0]);
                        _vm.endMonth = parseInt(moment(this.timeSpan[1]).format('MM-DD-YYYY').split('-')[0]);
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
            next() {
                var _vm = this;
                if (this.active <= 2) 
                {
                    if (this.active == 1) {
                    _vm.sum(_vm.infiledList);
                    _vm.word='Save';
                }
                if (this.active == 2) {
                    var _vm = this;
                    let project = this.form;
                    project.CloseDate = moment(project.CloseDate).format('MM-DD-YYYY');
                    project.StartDate = moment(project.StartDate).format('MM-DD-YYYY');
                    project.Country = _vm.form.Country;
                    project.Employees = _vm.infiledList;

                    for (let j in project.Employees) {
                        if (!project.Employees[j].Id) {
                            project.Employees[j].ProjectNo = _vm.form.ProjectNo;
                        }
                    }
                    project.ProjectFinancList = _vm.sumList;
                    for (let j in project.ProjectFinancList) {
                        //已存在，编辑
                        if (!project.ProjectFinancList[j].Id) {
                            project.ProjectFinancList[j].ProjectNo = _vm.form.ProjectNo;
                        }
                    }

                    formData_service.default.editProject.extc(project)
                        .then(() => {
                            this.$router.push({
                                name: 'project_edit',
                                params: {
                                    item: project.ProjectNo
                                }
                            })
                        })
                }
                this.active++;
                }

            },
            addEmployee() {
                var _vm = this;
                let warning = false;
                for (let i = _vm.startMonth; i <= _vm.endMonth; i++) {
                    this.checkList.forEach(element => {
                        let consultant = _vm.getProperty(this.consultants, 'Consultant_Name', element);
                        let exists = false;
                        _vm.infiledList.forEach(utilization => {
                            if (utilization.Consultant_Name == element && utilization.Month == _vm.MonthMapping(i)) {
                                exists = true;
                                warning = true;
                            }
                        })
                        if (!exists) {
                            this.infiledList.push({
                                Consultant_Id: consultant.Consultant_Id, Consultant_Name: consultant.Consultant_Name, Type: consultant.Type, CostRate: consultant.CostRate, Month: _vm.MonthMapping(i)
                            })
                        }
                    });
                }

                if (warning) {
                    console.log('已存在');
                    this.$message({
                        message: 'The item is already exits, please try another one',
                        type: 'warning'
                    });
                }
            },
            changeTimeSpan(){
                var _vm=this;
                _vm.startMonth = parseInt(moment(this.timeSpan[0]).format('MM-DD-YYYY').split('-')[0]);
                _vm.endMonth = parseInt(moment(this.timeSpan[1]).format('MM-DD-YYYY').split('-')[0]);
                let warning=false;
                for (let i = _vm.startMonth; i < _vm.endMonth + 1; i++) {
                    let exits=false;
                   _vm.FinancialReport.forEach(item=>{
                       if(item.Month== _vm.MonthMapping(i)){
                        warning=true;
                        exits=true;
                       }
                   })
                   if(!exits){
                    this.FinancialReport.push({ Month: _vm.MonthMapping(i) });
                   }
                   if(warning){
                    this.$message({
                        message: 'The Month is already exits, please try another one',
                        type: 'warning'
                    });
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

            deleteRow(index, rows, rowId) {//删除改行
                rows.splice(index, 1);
                formData_service.default.deleteEmployeeWorkdayDetail.exec(rowId)
            },
            deleteFinanceRow(index, rows, rowId) {
                rows.splice(index, 1);
                formData_service.default.deleteFinance.exec(rowId)
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
            sum(list) {
                var _vm = this;

                let groupList = _vm.groupBy(list, function (item) {
                    return [item.Month];
                });
                groupList.forEach(item => {
                    let HeadCountCost = 0;
                    let ChargesIn = 0;
                    let Contractors = 0;
                    let typeGroups = _vm.groupBy(item, function (innerItem) {
                        return [innerItem.Type];
                    })
                    typeGroups.forEach(typeItem => {
                        if (typeItem[0].Type == 'HC') {
                            if (typeItem.length > 1) {
                                HeadCountCost = typeItem.reduce(function (prev, curr, idx, arr) {

                                    return (parseInt(prev.CostRate) * parseInt(prev.WorkDays) + parseInt(curr.CostRate) * parseInt(curr.WorkDays)) * 8;
                                })
                            }
                            else if (typeItem.length = 1) {
                                HeadCountCost = (parseInt(typeItem[0].CostRate) * parseInt(typeItem[0].WorkDays)) * 8;
                            }

                        }
                        else if (typeItem[0].Type == 'EX') {
                            if (typeItem.length > 1) {
                                ChargesIn = typeItem.reduce(function (prev, curr, idx, arr) {
                                    return (parseInt(prev.CostRate) * parseInt(prev.WorkDays) + parseInt(curr.CostRate) * parseInt(curr.WorkDays)) * 8;
                                })
                            } else {
                                ChargesIn = (parseInt(typeItem[0].CostRate) * parseInt(typeItem[0].WorkDays)) * 8;
                            }

                        }
                        else if (typeItem[0].Type == 'CO') {
                            if (typeItem.length > 1) {
                                Contractors = typeItem.reduce(function (prev, curr, idx, arr) {
                                    return (parseInt(prev.CostRate) * parseInt(prev.WorkDays) + parseInt(curr.CostRate) * parseInt(curr.WorkDays)) * 8;
                                })
                            }
                            else {
                                Contractors = (parseInt(typeItem[0].CostRate) * parseInt(typeItem[0].WorkDays)) * 8;
                            }

                        }
                    })
                    let exits=false;
for(let i in _vm.FinancialReport){
    if(_vm.FinancialReport[i].Month==item[0].Month){
        exits=true;
        _vm.sumList.push(_vm.FinancialReport[i]);
    }
}
if(!exits){
    _vm.sumList.push({Month: item[0].Month, HeadCountCost: HeadCountCost, ChargesIn: ChargesIn, Contractors: Contractors });
}
                    
                });

                console.log(groupList);
            },

            getProperty(list, property, value) {
                for (let i in list) {
                    if (list[i][property] == value) {
                        return list[i];
                    }
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