<template>
        <div>
            <el-table :data="customerList" style="width: 100%" v-loading="loading">
                <el-table-column prop="Customer_name" label="Customer Name">
                </el-table-column>
                <el-table-column prop="Customer_Contact" label="Customer Contact">
                </el-table-column>
                <el-table-column fixed="right" label="Operation">
                    <template slot-scope="scope">
                        <el-button @click.native.prevent="editCustomer(scope.row.CustomerId)"
                            size="small"> Edit
                        </el-button>
                        <el-button @click.native.prevent="deleteCustomer(scope.$index, customerList,scope.row.CustomerId)"
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
                    customerList: [],
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
                formData_service.default.getAllCustomer.exec()
                    .then(data => {
                        this.customerList = data.data;
                        this.loading=false;
                    });     
              },
              methods: {  
                  deleteCustomer(index, rows, rowId) {
                      this.$confirm('Do you want to delete this customer,this may delete the relation project?', '', {
                          confirmButtonText: 'Yes',
                          cancelButtonText: 'No',
                          type: 'warning'
                      }).then(() => {
                          formData_service.default.deleteCustomer.exec(rowId)
                              .then(()=>{
                                rows.splice(index, 1);
                                // this.customerList.forEach((element,index) => {
                                //       if(element.CustomerId==rowId){
                                //         this.customerList.splice(index,1);
                                //       }
                                //     });                                  
                              });
                      })

                  },
                  editCustomer(rowId) {
                      this.$router.push({
                          name: 'customer_edit',
                          params: {
                              item: rowId
                          }
                      })
                  }
              }
            }
          </script>