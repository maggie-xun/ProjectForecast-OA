<template>
    <div>
            <el-table border :data="FinancialReport" style="width: 100%">
                    <el-table-column prop="Month" label="Month" style="width:6vw;">
                        <template scope="scope">
                            <el-input size="mini" v-model="scope.row.Month" disabled="disabled"></el-input>
                        </template>
                    </el-table-column>
                    <el-table-column prop="HeadCountCost" label="HeadCountCost" style="width:6vw;">
                        <template scope="scope">
                            <el-input size="mini" v-model="scope.row.HeadCountCost" disabled="disabled">
                            </el-input>
                        </template>
                    </el-table-column>
                    <el-table-column prop="ChargesIn" label="ChargesIn" style="width:6vw;">
                        <template scope="scope">
                            <el-input size="mini" v-model="scope.row.ChargesIn" disabled="disabled"></el-input>
                        </template>
                    </el-table-column>
                    <el-table-column prop="Contractors" label="Contractors" style="width:6vw;">
                        <template scope="scope">
                            <el-input size="mini" v-model="scope.row.Contractors" disabled="disabled">
                            </el-input>
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
</template>
<script>
    var formData_service = require('./../../services/form-data-service');
    export default {
        data() {
            return {
                FinancialReport: [],
            }
        },
        created() {
            var _vm = this;
            _vm.loading = true;
            _vm.projectId = this.$route.params.item;
            formData_service.default.getProjectPerId.exec(_vm.projectId)
                .then(data => {
                    _vm.detailData = data.data;
                    _vm.form = _vm.detailData;
                    if (_vm.form.Country == null) {
                        _vm.form.Country = { CountryName: "" };
                    }
                    if (_vm.form.Consultant == null) {
                        _vm.form.Consultant = { Consultant_Name: "" };
                    }
                    if (_vm.form.Customer == null) {
                        _vm.form.Customer = { Customer_name: "" };
                    }

                    _vm.totalUtilization = _vm.detailData.TeamUtilization;

                    _vm.infiledList = _vm.detailData.Employees;
                    _vm.FinancialReport = _vm.detailData.ProjectFinancList;
                    _vm.loading = false;
                });
        },
    }
</script>