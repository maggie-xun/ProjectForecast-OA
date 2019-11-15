<template>
    <div>
        <el-table :data="employeeList" style="width: 100%" v-loading="loading">
            <el-table-column prop="Consultant_Name" label="Consultant Name">
            </el-table-column>
            <el-table-column prop="Type" label="Type">
            </el-table-column>
            <el-table-column prop="CostRate" label="CostRate">
            </el-table-column>
            <el-table-column prop="HireDecision" label="Hire Decision">
            </el-table-column>
            <el-table-column prop="TimeSchedule" label="Time Schedule">
            </el-table-column>
            <el-table-column fixed="right" label="Operation">
                <template slot-scope="scope">
                    <el-button @click.native.prevent="EditEmployee(scope.row.Consultant_Id)" size="small"> Edit
                    </el-button>
                    <el-button
                        @click.native.prevent="deleteEmployee(scope.$index, employeeList,scope.row.Consultant_Id)"
                        size="small"> Remove
                    </el-button>
                </template>
            </el-table-column>
        </el-table>
    </div>
</template>
<script>
    var formData_service = require('./../../services/form-data-service');
    export default {
        data() {
            return {
                employeeList: [],
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
            formData_service.default.getAllEmployee.extc()
                .then(data => {
                    this.employeeList = data.data;
                    this.loading = false;
                });
        },
        methods: {       
            deleteEmployee(index, rows, rowId) {
                this.$confirm('Do you want to delete this consultant, this may delete the relation project?', '', {
                    confirmButtonText: 'Yes',
                    cancelButtonText: 'No',
                    type: 'warning'
                }).then(() => {
                    
                    formData_service.default.deleteEmployee.exec(rowId).then(data => {
                        rows.splice(index, 1);
                        // this.customerList.forEach((element,index) => {
                        //               if(element.CustomerId==rowId){
                        //                 this.customerList.splice(index,1);
                        //               }
                        //             });   
                    });

                }).catch(() => {
                    this.$message({
                        type: 'info',
                        message: 'delete cancled'
                    });
                })
            },
            EditEmployee(rowId) {
                this.$router.push({
                    name: 'employee_edit',
                    params: {
                        item: rowId
                    }
                })
            }
        }
    }
</script>