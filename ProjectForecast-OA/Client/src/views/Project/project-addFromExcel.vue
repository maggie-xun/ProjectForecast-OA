<template>
    <div>
        <input id='input-file' style="display:inline-block;width:50%" type="file" @change="upload()" />
        <button @click="submit">导入文件</button>
        <el-button type="primary" @click="submit" style="float:right">导入文件</el-button>
        <project_list></project_list>
    </div>
</template>
<script>
    var formData_service = require('./../../services/form-data-service');
    var project_list=require('./project-list').default;
    export default {
        components:{
            project_list
        },
        data() {
            return {
                fileList: [],
            };
        },
        methods: {
            upload(obj) {
                var _vm = this;
                _vm.fileList = event.target.files;
                // console.log('path'+obj.value);
                // var file=document.getElementById('input-file').files[0];
               
            },

            submit() {
                var _vm = this;
                _vm.flag = true;
               
                var filePath=document.getElementById('input-file').value;

                formData_service.default.importFromExcel.exec(_vm.fileList,"ImportProjectFromExcel")
                    .done(function (read_response) {
                        // read_response = JSON.parse(read_response);
                        // read_response.forEach(story => {
                            
                        // });
                        window.location.reload();
                        _vm.flag = false;
                    })

            }
        }
    }
</script>