<template>
  <div>
    <div class="side-nav">
      <el-table :data="treeList" style="width: 100%" v-loading="loading">
        <el-table-column prop="Type" label="Type" width="60">
        </el-table-column>
        <el-table-column prop="Status" label="Status">
        </el-table-column>
        <el-table-column prop="Country.CountryName" label="Country">
        </el-table-column>
        <el-table-column prop="ProjectNo" label="ProjectNo" >
        </el-table-column>
        <el-table-column prop="Customer.Customer_name" label="Customer">
        </el-table-column>
        <el-table-column prop="ProjectName" label="Project Name" width="128">
        </el-table-column>
        <el-table-column prop="Consultant.Consultant_Name" label="Project Manager" width="130">
        </el-table-column>
        <el-table-column prop="StartDate" label="StartDate">
          </el-table-column>
        <el-table-column prop="CloseDate" label="CloseDate">
        </el-table-column>
        <el-table-column prop="Comment" label="Comment" >
        </el-table-column>
        <el-table-column fixed="right" label="Operation" width="150">
          <template slot-scope="scope">
            <el-button @click="getProjectDetails(scope.row)" type="text" size="small">Detail</el-button>
            <el-button @click='editProjectDetails(scope.row)' type="text" size="small">Edit</el-button>
            <el-button @click='deleteProjectDetails(scope.row)' type="text" size="small">Delete</el-button>
          </template>
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
        projects: [],
        projectFinance: [],
      }
    },
    created() {
      this.loading = true;
      formData_service.default.getProjects.extc()
        .then(data => {
          this.treeList = data.data;
          this.loading = false;
        });
      console.log(this.treeList);

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
      getProjectDetails(row) {
        this.$router.push({
          name: 'project_detail',
          params: {
            item: row.ProjectNo
          }
        })
      },
      editProjectDetails(row) {
this.$router.push({
  name: 'project_edit',
          params: {
            item: row.ProjectNo
          }
})
      },
      deleteProjectDetails(row){
        formData_service.default.deleteProjectPerId.exec(_vm.projectId)
        .then(result=>{

        })
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