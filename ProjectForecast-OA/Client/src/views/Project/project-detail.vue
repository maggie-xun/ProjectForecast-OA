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
                        <el-table-column prop="Jan" label="Jan">
                            <template scope="scope">
                                <el-input size="mini" v-model="scope.row.WorkingMonth.Jan" disabled="disabled"></el-input>
                            </template>
                        </el-table-column>
                        <el-table-column prop="Feb" label="Feb">
                            <template scope="scope">
                                <el-input size="mini" v-model="scope.row.WorkingMonth.Feb" disabled="disabled"></el-input>
                            </template>
                        </el-table-column>
                        <el-table-column prop="Mar" label="Mar">
                            <template scope="scope">
                                <el-input size="mini" v-model="scope.row.WorkingMonth.Mar" disabled="disabled"></el-input>
                            </template>
                        </el-table-column>
                        <el-table-column prop="Apr" label="Apr">
                            <template scope="scope">
                                <el-input size="mini" v-model="scope.row.WorkingMonth.Apr" disabled="disabled"></el-input>
                            </template>
                        </el-table-column>
                        <el-table-column prop="May" label="May">
                            <template scope="scope">
                                <el-input size="mini" v-model="scope.row.WorkingMonth.May" disabled="disabled"></el-input>
                            </template>
                        </el-table-column>
                        <el-table-column prop="Jun" label="Jun">
                            <template scope="scope">
                                <el-input size="mini" v-model="scope.row.WorkingMonth.Jun" disabled="disabled"></el-input>
                            </template>
                        </el-table-column>
                        <el-table-column prop="Jul" label="Jul">
                            <template scope="scope">
                                <el-input size="mini" v-model="scope.row.WorkingMonth.Jul" disabled="disabled"></el-input>
                            </template>
                        </el-table-column>
                        <el-table-column prop="Aug" label="Aug">
                            <template scope="scope">
                                <el-input size="mini" v-model="scope.row.WorkingMonth.Aug" disabled="disabled"></el-input>
                            </template>
                        </el-table-column>
                        <el-table-column prop="Sep" label="Sep">
                            <template scope="scope">
                                <el-input size="mini" v-model="scope.row.WorkingMonth.Sep" disabled="disabled"></el-input>
                            </template>
                        </el-table-column>
                        <el-table-column prop="Oct" label="Oct">
                            <template scope="scope">
                                <el-input size="mini" v-model="scope.row.WorkingMonth.Oct" disabled="disabled"></el-input>
                            </template>
                        </el-table-column>
                        <el-table-column prop="Nov" label="Nov">
                            <template scope="scope">
                                <el-input size="mini" v-model="scope.row.WorkingMonth.Nov" disabled="disabled"></el-input>
                            </template>
                        </el-table-column>
                        <el-table-column prop="Dec" label="Dec">
                            <template scope="scope">
                                <el-input size="mini" v-model="scope.row.WorkingMonth.Dec" disabled="disabled"></el-input>
                            </template>
                        </el-table-column>
                    </el-table>

                    <project_utilization v-if="totalUtilization.length>0" v-bind:utilizationData="totalUtilization"
                        v-bind:projectNo='projectId'></project_utilization>
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
                </el-tab-pane>
            </el-tabs>
        </div>
    </div>
</template>
<script>
    var formData_service = require('./../../services/form-data-service');
    var project_utilization = require('./utilization_all_column').default;
    var moment = require('moment');
    export default {
        components: {
            project_utilization: () => import('./utilization_all_column')
        },
        data() {
            return {
                detailData: {},
                form: { Country: { CountryName: "" }, Consultant: { Consultant_Name: "" }, Customer: { Customer_name: "" } },
                infiledList: [],
                activeName: 'first',
                loading: false,
                FinancialReport: [],
                totalUtilization: []
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
                    if (_vm.form.Consultant == null) {
                        _vm.form.Country = { CountryName: "" };
                    }
                    if(_vm.form.Country == null){
                        _vm.form.Country = { CountryName: "" };
                    }
                    if(_vm.form.Customer == null){
                        _vm.form.Customer = { Customer_name: "" };
                    }

                    _vm.totalUtilization = _vm.detailData.TeamUtilization;

                    _vm.infiledList = _vm.detailData.Employees;
                    _vm.FinancialReport = _vm.detailData.ProjectFinancList;
                    _vm.loading = false;
                });
        },
        methods: {
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


    #project_column {
        width: 600px;
        height: 400px;
        border: 1px solid red;
        display: inline-block;
    }

    #monthly_pie {
        width: 600px;
        height: 400px;
        border: 1px solid red;
        display: inline-block;
    }

    .el-table td {
        padding: 0;
    }

    .el-table .cell,
    .el-table th div {
        padding: 0
    }

    .el-table .cell,
    .el-table th div,
    .el-table--border td:first-child .cell,
    .el-table--border th:first-child .cell {
        padding: 0;
    }
</style>