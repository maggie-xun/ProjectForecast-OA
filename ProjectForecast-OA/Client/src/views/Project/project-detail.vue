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
                                <el-form-item>
                                    <el-button type="primary">Add Now</el-button>
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
                                        <el-table-column prop="CostRate" label="WorkDays" disabled="disabled">
                                            <template scope="scope">
                                                <el-input size="mini" v-model="scope.row.WorkDays"></el-input>
                                            </template>
                                        </el-table-column>
                                    </el-table>
                            <el-button type="primary">Save</el-button>

                        </el-tab-pane>
                        <el-tab-pane label="FinancialReport" name="third">
                            <div class="block">        
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
                                </el-table>
                            </div>
                        </el-tab-pane>
                    </el-tabs>
          </div>
        </div>
      </template>
      <script>
        var formData_service = require('./../../services/form-data-service');
        export default {
          data() {
            return {
              detailData:{ },
              form: {},
              infiledList:[],
              activeName: 'first',
              loading: false,       
              projects:[],
              projectFinance:[],
              FinancialReport:[]
            }
          },
          created() {
            var _vm=this;
            _vm.loading=true;
            _vm.projectId = this.$route.params.item;
            formData_service.default.getProjectPerId.exec(_vm.projectId)
              .then(data => {
                _vm.detailData = data.data[0];
                _vm.form=_vm.detailData;
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
          methods: {
            MonthMapping(month){
      switch(month){
        case "1":return "Mon";
        case "2":return "Feb";
        case "3":return "Mar";
        case "4":return "Apr";
        case "5":return "May";
        case "6":return "Jun";
        case "7":return "Jul";
        case "8":return "Aug";
        case "9":return "Sep";
        case "10":return "Oct";
        case "11":return "Sep";
        case "12":return "Dec";
      }
            },
            NextPage(){
                this.activeName='second';
            },
            handleNodeClick(data) {
              console.log(data);
            },
            handleClick(tab, event) {
              console.log(tab, event);
            },
           
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
      </style>