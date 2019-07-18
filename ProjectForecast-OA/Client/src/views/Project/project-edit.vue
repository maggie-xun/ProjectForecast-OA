<template>
        <div>
            <div class="side-nav">
                <el-tabs v-model="activeName" type="border-card" style='line-height: 40px;'>
                    <el-tab-pane label="Basic Info" name="first" style='line-height: 20px;'>
                        <el-form ref="form" :model="form" label-width="120px">
                            <el-form-item label="Country">
                                <el-select v-model="form.Country.CountryId" placeholder="Please Select a Country">
                                    <el-option :label="country.CountryName" :value="country.CountryId" v-for='country in Countries'></el-option>
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
                                        <el-option :label="consultant.Consultant_Name" :value="consultant.Consultant_ID" v-for='consultant in consultants'></el-option>
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
                                <el-button type="primary">Save Now</el-button>
                                <el-button type="primary" @click='NextPage'>Next Tab</el-button>
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
                            <el-button type="primary" @click="EditConsultant">AddEmployee</el-button>
                        </div>
                        <el-table border :data="infiledList" style="width: 100%">
                            <el-table-column prop="fildna" label="Name" style="width:6vw;">
                                <template scope="scope">
                                    <el-input size="mini" v-model="scope.row.Consultant_Name" disabled="disabled"></el-input>
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
                    customers:[],
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
                var _vm=this;
                formData_service.default.getAllEmployee.extc()
                    .then(data => {
                        this.consultants = data.data;
                    });
                formData_service.default.getAllCountry.extc()
                    .then(data => {
                        this.Countries = data.data;
                    });

                    formData_service.default.getAllCustomer.exec()
                .then((data)=>{
                    this.customers=data.data;
                })

                    _vm.projectId = this.$route.params.item;
            formData_service.default.getProjectPerId.exec(_vm.projectId)
              .then(data => {
                _vm.detailData = data.data[0];
                _vm.form=_vm.detailData;
                if(! _vm.form.Country){
                    _vm.form.Country={CountryId:''}
                }
                _vm.infiledList=_vm.detailData.Employees;
                _vm.FinancialReport=_vm.detailData.ProjectFinancList;
                // _vm.treeList.forEach(element => {
                //   let tree={"Type":element.Type,"Status":element.Status,"Country":element.Country,"ProjectNo":element.ProjectNo,"Customer":element.Customer,"ProjectName":element.ProjectName,"CloseDate":element.CloseDate}
                //   element.ProjectFinancList.forEach(project=>{
                //     let monthTemp=_vm.MonthMapping(project.Month);
                //     tree[monthTemp]=project.Revenue;
                //   });
                //   _vm.projectFinance.push(tree);
                // });
                // this.projectFinance=
                _vm.loading=false;
                console.log(_vm.form);
              });
           
            },
            computed: {
    
            },
            watch: {
    
            },
            methods: {
                MonthMapping(month) {
        switch (month) {
          case "1": return "Mon";
          case "2": return "Feb";
          case "3": return "Mar";
          case "4": return "Apr";
          case "5": return "May";
          case "6": return "Jun";
          case "7": return "Jul";
          case "8": return "Aug";
          case "9": return "Sep";
          case "10": return "Oct";
          case "11": return "Sep";
          case "12": return "Dec";
        }
      },
                NextPage(){
                    this.activeName='second';
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
                    let project = this.form;
                    project.CloseDate = moment(project.CloseDate).format('MM-DD-YYYY');
                    project.StartDate = moment(project.CloseDate).format('MM-DD-YYYY');
                    project.Country=this.form.Country;
                    if (project.ProjectFinancList.length <= 0) {
                        project.ProjectFinancList = [];
                    }
                    if (project.Employees.length <= 0) {
                        project.Employees = [];
                    }

                    this.infiledList.forEach(element => {
                        if(this.months.length>0){
                            this.months.forEach(month => {
                            let workingHour;
                            element.MonthElement.forEach(monthElement => {
                                if (monthElement.Month == month) {
                                    workingHour = monthElement.WorkingHour;
                                }
                            })
    
                            project.Employees.push({ Consultant_Id: element.Consultant_Id, ProjectNo: this.form.ProjectNo, Month: month, WorkDays: workingHour })
                        })
                        }
                        else{
                            project.Employees.forEach(employee => {
                                if(element.Consultant_Id==employee.Consultant_Id){
                                    employee=element;
                                }
                            });
                        }
                        
    
                    });
                    this.FinancialReport.forEach(element => {
                        if(this.FinancialReportTimeSpan.length>0){
                            this.FinancialReportTimeSpan.forEach(month => {
                            let financeMonth = parseInt(moment(month).format('MM-DD-YYYY').split('-')[0]);
                            let financeYear=parseInt(moment(month).format('MM-DD-YYYY').split('-')[2]);
                            project.ProjectFinancList.push({
                                ProjectNo: this.form.ProjectNo, Revenue: element.Revenue, Expenses: element.Expenses,
                                IT: element.IT, Materials: element.Materials, Month: financeMonth,Year:financeYear
                            })
                        })
                        }
                       else{
                        project.ProjectFinancList.forEach(projectFinance => {
                            if(element.Id==projectFinance.Id){
                                projectFinance=element;
                            }
                       })
                    } 
                    });
                    formData_service.default.editProject.extc(project)
                },
                changeFinancialReportTimeSpan() {
                    let startMonth = parseInt(moment(this.FinancialReportTimeSpan[0]).format('MM-DD-YYYY').split('-')[0]);
    
                    let endMonth = parseInt(moment(this.FinancialReportTimeSpan[1]).format('MM-DD-YYYY').split('-')[0]);
                    for (let i = startMonth; i < endMonth + 1; i++) {
                        this.FinancialReport.push({ Month: i + '' });
                    }
                },
                changeUtilizationTimeSpan() {
                    let startMonth = parseInt(moment(this.timeSpan[0]).format('MM-DD-YYYY').split('-')[0]);
                    let endMonth = parseInt(moment(this.timeSpan[1]).format('MM-DD-YYYY').split('-')[0]);
                    for (let i = startMonth; i <= endMonth; i++) {
                        this.months.push(this.MonthMapping(i + ''));
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