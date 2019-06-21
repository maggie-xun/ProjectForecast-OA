<template>
  <div>
    <!-- <el-button type="primary">Add Project</el-button> -->
    <!-- <el-tree :data="treeList"  @node-click="handleNodeClick"></el-tree> -->
    <div class="side-nav">
      <!-- <ul>
                <li v-for='item in treeList'>{{item.label}}</li>
              </ul> -->
      <el-tabs tab-position="left">
        <el-tab-pane :label="item.ProjectName" v-for='item in treeList'>
          <el-tabs v-model="activeName" type="card" @tab-click="handleClick" style='line-height: 40px;'>
            <el-tab-pane label="Basic Info" name="first" style='line-height: 20px;'>
              <el-form ref="form" :model="form" label-width="120px">
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
      </el-tabs>
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
        activeName: 'first'
      }
    },
    created() {
      formData_service.default.getProjects.extc()
        .then(data => {
          this.treeList = data.data;
        })
      console.log(this.treeList);

    },
    methods: {
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