<template>
    <div>
        <div>
            <input id='input-file' style="display:inline-block;width:50%" type="file" @change="upload()" />
            <button @click="submit">导入文件</button>
            <el-button type="primary" @click="submit" style="float:right">导入文件</el-button>
        </div>


        <el-table :data="crmList" style="width: 100%" v-loading="loading" >
            <el-table-column  label="Opportunity Name">
                <template slot-scope="scope">
                        <a v-bind:href="scope.row.url">{{scope.row.OpportunityName}}</a>
                </template>
                   
            </el-table-column>
            <el-table-column prop="Country" label="Country/NWA (Account)">
            </el-table-column>
            <el-table-column prop="Owner" label="Owner">
            </el-table-column>
            <el-table-column prop="ClosePlanState" label="Close Plan State">
            </el-table-column>
            <el-table-column prop="CloseDate" label="Close Date">
            </el-table-column>
            <el-table-column prop="EstRevenue" label="Est. Revenue">
            </el-table-column>
            <el-table-column prop="ProfessionalServicesDeal" label="Professional Services Deal">
            </el-table-column>
            <el-table-column prop="ModifiedBy" label="Modified By">
            </el-table-column>
            <el-table-column prop="ModifiedOn" label="Modified On">
            </el-table-column>
            <el-table-column prop="ServicesLead" label="Services Lead">
            </el-table-column>
            <el-table-column prop="OpportunityID" label="(opportunityid)">
            </el-table-column>
            <el-table-column prop="Currency" label="Currency">
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
                crmList: []
            }
        },
        created() {
            this.loading = true;
            formData_service.default.getCRMs.extc()
                .then(data => {
                    this.crmList = data.data;
                    if(this.crmList!=""){
                        this.crmList.forEach(element => {
                        element.CloseDate=new Date(Number(element.CloseDate.slice(6,19))).toDateString();
                        element.ModifiedOn=new Date(Number(element.ModifiedOn.slice(6,19))).toDateString();
                        element.url="http://crm.corp.halliburton.com/SalesMethod/main.aspx?etcz3&id=%7b"+element.Opportunity_ID+"%7d&pagetype=entityrecord"
                    });
                    }
                    else{
                        this.crmList=[]
                    }

                    this.loading = false;
                });
        },
        methods: {
            upload(obj) {
                var _vm = this;
                _vm.fileList = event.target.files;
            },

            submit() {
                var _vm = this;
                _vm.flag = true;

                var filePath = document.getElementById('input-file').value;

                formData_service.default.importFromExcel.exec(_vm.fileList, "ImportCRM")
                    .done(function (read_response) {
                        // window.location.reload();
                        __vm.$router.push({
                            name:'crm'
                        })
                        _vm.flag = false;
                    })

            }
        }
    }

</script>