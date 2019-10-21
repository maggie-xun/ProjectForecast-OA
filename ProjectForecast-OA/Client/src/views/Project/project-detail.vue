<template>
        <div>
          <div class="side-nav">
            <el-tabs v-model="activeName" type="border-card" style='line-height: 40px;'>
                    <el-tab-pane label="Basic Info" name="first" style='line-height: 20px;'>
                        <el-form ref="form" :model="form" label-width="120px">
                                <el-form-item label="Country">
                                    <el-input v-model="form.Country.CountryName" :disabled="true"></el-input>
                                </el-form-item>
                                <el-form-item label="Project Number">
                                    <el-input v-model="form.ProjectNo" :disabled="true"></el-input>
                                </el-form-item>
                                <el-form-item label="Project Name">
                                    <el-input v-model="form.ProjectName" :disabled="true"></el-input>
                                </el-form-item>
                                <el-form-item label="Project Manager">
                                        <el-input v-model="form.Consultant.Consultant_Name" :disabled="true"></el-input>
                                    </el-form-item>
                                <el-form-item label="Customer">
                                    <el-input v-model="form.Customer.Customer_name" :disabled="true"></el-input>
                                </el-form-item>
                                <el-form-item label="Type">
                                    <el-input v-model="form.Type" :disabled="true"></el-input>
                                </el-form-item>
                                <el-form-item label="Status">
                                        <el-input v-model="form.Status" :disabled="true"></el-input>
                                </el-form-item>
                                <el-form-item label="CloseDate">
                                        <el-input v-model="form.CloseDate" :disabled="true"></el-input>
                                </el-form-item>
                        </el-form>
                    </el-tab-pane>
                    <el-tab-pane label="Utilization" name="second">
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
                                                        <el-input size="mini" v-model="scope.row.MonthElement[index].WorkingHour" disabled="disabled"></el-input>
                                                    </template>
                                                </el-table-column>
                                </el-table>
                                <project_utilization v-if="totalUtilization.length>0" v-bind:utilizationData="totalUtilization" v-bind:projectNo='projectId'></project_utilization>
                    </el-tab-pane>
                    <el-tab-pane label="FinancialReport" name="third">
                        <div class="block">        
                                <el-table border :data="FinancialReport" style="width: 100%">
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
                                                <el-input size="mini" v-model="scope.row.GP" disabled="disabled"></el-input>
                                            </template>
                                        </el-table-column>
                                    <el-table-column prop="Revenue" label="Revenue" style="width:6vw;">
                                        <template scope="scope">
                                            <el-input size="mini" v-model="scope.row.Revenue" disabled="disabled"></el-input>
                                        </template>
                                    </el-table-column>
                                    <el-table-column prop="Expenses" label="Expenses">
                                        <template scope="scope">
                                            <el-input size="mini" v-model="scope.row.Expenses" disabled="disabled"></el-input>
                                        </template>
                                    </el-table-column>
                                    <el-table-column prop="CostRate" label="IT(HW/SW)">
                                        <template scope="scope">
                                            <el-input size="mini" v-model="scope.row.IT" disabled="disabled"></el-input>
                                        </template>
                                    </el-table-column>
                                    <el-table-column prop="Materials" label="Materials">
                                        <template scope="scope">
                                            <el-input size="mini" v-model="scope.row.Materials" disabled="disabled"></el-input>
                                        </template>
                                    </el-table-column>                                 
                                </el-table>
                        </div>
                    </el-tab-pane>
            </el-tabs>
          </div>
        </div>
      </template>
      <script>
        var formData_service = require('./../../services/form-data-service');
        var project_utilization=require('./utilization_all_column').default;
        var moment = require('moment');
        export default {
            components:{
                project_utilization:()=>import('./utilization_all_column')
            },
          data() {
            return {
              detailData:{ },
              form: {Country:{CountryName:""},Consultant:{Consultant_Name:""},Customer:{Customer_name:""}},
              infiledList:[],
              activeName: 'first',
              loading: false,       
              projects:[],
              projectFinance:[],
              FinancialReport:[],
              months: [],
              timeSpan:[],
              startMonth:0,
              endMonth:0,
              FinancialReportMonths: [],
              totalUtilization:[]
            }
          },
          created() 
              
          {
            var _vm=this;
            _vm.loading=true;
            _vm.projectId = this.$route.params.item;
            formData_service.default.getProjectPerId.exec(_vm.projectId)
              .then(data => {
                  _vm.detailData = data.data;
                  _vm.form = _vm.detailData;
                  if(_vm.form.Consultant==null){
                      _vm.form.Consultant={ConsultantName:""};
                  }
                  _vm.totalUtilization=_vm.detailData.TeamUtilization;
                  _vm.timeSpan = [_vm.form.StartDate, _vm.form.CloseDate];
                  // _vm.infiledList=_vm.detailData.Employees;
                  _vm.FinancialReport = _vm.detailData.ProjectFinancList;
                  _vm.startMonth = parseInt(moment(this.timeSpan[0]).format('MM-DD-YYYY').split('-')[0]);
                  _vm.endMonth = parseInt(moment(this.timeSpan[1]).format('MM-DD-YYYY').split('-')[0]);
                  for (let i = _vm.startMonth; i <= _vm.endMonth; i++) {
                      this.months.push(i);
                  }
                  let result = _vm.groupBy(_vm.detailData.Employees, function (item) {
                      return [item.Consultant_Id];
                  })

                  result.forEach(employee => {
                      let consultant_show = { Id: employee[0].Id, Consultant_Id: employee[0].Consultant_Id, Consultant_Name: employee[0].Consultant_Name, Type: employee[0].Type, CostRate: employee[0].CostRate, ProjectNo: this.form.ProjectNo, Month: employee[0].Month, };
                      let MonthElement = [];
                      employee.forEach(monthElement => {
                          let exits = false;
                          if(_vm.months.length==0){
                            //   _vm.months=['Jan','Feb','Mar','Apr','May','Jun','Jul','Aug','Sep','Oct','Nov','Dec'];
                            _vm.months=[1,2,3,4,5,6,7,8,9,10,11,12]
                          }
                          for (let j in _vm.months) {
                              if (_vm.months[j] == _vm.MonthMappingReverse(monthElement.Month)) {
                                  exits = true;
                                  if ((_vm.getProperty(MonthElement, 'Month', _vm.MonthMapping(_vm.months[j]))) && (_vm.getProperty(MonthElement, 'Month', _vm.MonthMapping(_vm.months[j])).WorkingHour == 0)) {
                                      MonthElement.forEach(a => {
                                          if (a.Month == monthElement.Month) {
                                              a.WorkingHour = monthElement.WorkDays;
                                          }
                                      })

                                  }
                                  else if (!(_vm.getProperty(MonthElement, 'Month', _vm.MonthMapping(_vm.months[j])))) {
                                      MonthElement.push({ Month: monthElement.Month, WorkingHour: monthElement.WorkDays })
                                  }
                              }
                              else {
                                  if (!(_vm.getProperty(MonthElement, 'Month', _vm.MonthMapping(_vm.months[j])))) {
                                      MonthElement.push({ Month: _vm.MonthMapping(_vm.months[j]), WorkingHour: 0 })
                                  }

                              }

                          }
                      });
                      consultant_show.MonthElement = MonthElement;
                      _vm.infiledList.push(consultant_show);
                  });
                  _vm.months.sort();


                _vm.loading=false;
              });
           
      
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
            handleClick(tab,event){
                // _vm.totalUtilization=_vm.detailData.TeamUtilization;


               
            }
          }
        }
      </script>
      <style>
        .side-nav {
          height: 100%;
      
          padding-right: 0;
        }
      
        .side-nav>ul {
          padding-bottom: 50px;
        }
      
        .side-nav li {
          list-style: none;
          height: 40p;
        }

        
  #project_column{
    width: 600px;
    height: 400px;
    border: 1px solid red;
    display:inline-block;
  }
  #monthly_pie{
    width: 600px;
    height: 400px;
    border: 1px solid red;
    display:inline-block;
  }
      </style>