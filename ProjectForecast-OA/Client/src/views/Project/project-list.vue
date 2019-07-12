<template>
  <div>
    <div class="side-nav">
      <!-- <el-tabs tab-position="left">
        <el-tab-pane :label="item.ProjectName" v-for='item in treeList'>
          <el-tabs v-model="activeName" type="card" @tab-click="handleClick" style='line-height: 40px;'>
            <el-tab-pane label="Basic Info" name="first" style='line-height: 20px;'>
              <el-form ref="form" :model="form" label-width="120px" v-loading="loading">
                <el-form-item label="Country">
                  <el-input placeholder="China" v-model="item.Country" :disabled="true" style="width: 200px;">
                  </el-input>
                </el-form-item>
                <el-form-item label="Project Number">
                  <el-input placeholder="China" v-model="item.ProjectNo" :disabled="true" style="width: 200px;">
                  </el-input>
                </el-form-item>
                <el-form-item label="Project Name">
                  <el-input placeholder="China" v-model="item.ProjectName" :disabled="true" style="width: 200px;">
                  </el-input>
                </el-form-item>
                <el-form-item label="Customer">
                  <el-input placeholder="China" v-model="item.Customer" :disabled="true" style="width: 200px;">
                  </el-input>
                </el-form-item>
                <el-form-item label="Type">
                  <el-input placeholder="China" v-model="item.Type" :disabled="true" style="width: 200px;">
                  </el-input>
                </el-form-item>
                <el-form-item label="Status">
                  <el-input placeholder="China" v-model="item.Status" :disabled="true" style="width: 200px;">
                  </el-input>
                </el-form-item>
                <el-form-item label="Close Date">
                  <el-input placeholder="China" v-model="item.CloseDate" :disabled="true" style="width: 200px;">
                  </el-input>
                </el-form-item>
              </el-form>
            </el-tab-pane>
            <el-tab-pane label="Utilization" name="second">配置管理</el-tab-pane>
            <el-tab-pane label="FinancialReport" name="third">角色管理</el-tab-pane>
          </el-tabs>
        </el-tab-pane>
      </el-tabs> -->

      <el-table :data="projectFinance" style="width: 100%">
        <el-table-column fixed prop="Type" label="Type" width="80">
        </el-table-column>
        <el-table-column fixed prop="Status" label="Status" width="80">
        </el-table-column>
        <el-table-column fixed prop="Country" label="Country" width="80">
        </el-table-column>
        <el-table-column fixed prop="ProjectNo" label="ProjectNo" width="100">
        </el-table-column>
        <el-table-column fixed prop="Customer" label="Customer" width="100">
        </el-table-column>
        <el-table-column fixed prop="ProjectName" label="ProjectName" width="120">
        </el-table-column>
        <el-table-column fixed prop="CloseDate" label="CloseDate" width="120">
        </el-table-column>
        <!-- <el-table-column
                  fixed
                    prop="CRM Rev"
                    label="CloseDate"
                    width="80">
                  </el-table-column> -->
        <el-table-column prop="Jan" label="Jan" width="80">
        </el-table-column>
        <el-table-column prop="Feb" label="Feb" width="80">
        </el-table-column>
        <el-table-column prop="Mar" label="Mar" width="80">
        </el-table-column>
        <el-table-column prop="Apr" label="Apr" width="80">
        </el-table-column>
        <el-table-column prop="May" label="May" width="80">
        </el-table-column>
        <el-table-column prop="Jun" label="Jun" width="80">
        </el-table-column>
        <el-table-column prop="Jul" label="Jul" width="80">
        </el-table-column>
        <el-table-column prop="Aug" label="Aug" width="80">
        </el-table-column>
        <el-table-column prop="Sep" label="Sep" width="80">
        </el-table-column>
        <el-table-column prop="Oct" label="Oct" width="80">
        </el-table-column>
        <el-table-column prop="Nov" label="Nov" width="80">
        </el-table-column>
        <el-table-column prop="Dec" label="Dec" width="80">
        </el-table-column>
      </el-table>
    </div>
  </div>
</template>
<script>
  var formData_service = require('./../../services/form-data-service');
  export default {
    data() {
      return {
        treeList: [],
        form: {
          name: ","
        },
        activeName: '',
        loading: false,       
        projects:[],
        projectFinance:[],
      }
    },
    created() {
      this.loading=true;
      formData_service.default.getProjects.extc()
        .then(data => {
          this.treeList = data.data;
          this.treeList.forEach(element => {
            let tree={"Type":element.Type,"Status":element.Status,"Country":element.Country,"ProjectNo":element.ProjectNo,"Customer":element.Customer,"ProjectName":element.ProjectName,"CloseDate":element.CloseDate}
            element.ProjectFinancList.forEach(project=>{
              let monthTemp=this.MonthMapping(project.Month);
              tree[monthTemp]=project.Revenue;
            });
            this.projectFinance.push(tree);
          });
          // this.projectFinance=
          this.loading=false;
        });
      console.log(this.treeList);

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

      handleNodeClick(data) {
        console.log(data);
      },
      handleClick(tab, event) {
        console.log(tab, event);
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
</style>