<template>
    <div>
        <input id='input-file' style="display:inline-block;width:50%" type="file" @change="upload()" />
        <button @click="submit">导入文件</button>
        <el-button type="primary" @click="submit" style="float:right">导入文件</el-button>
    </div>
</template>

<script>
     var formData_service = require('./../../services/form-data-service');
     export default {
        data() {
         return{}
        },
        methods: {
            upload(obj) {
                var _vm = this;
                _vm.fileList = event.target.files;               
            },

            submit() {
                var _vm = this;
                _vm.flag = true;
               
                var filePath=document.getElementById('input-file').value;

                formData_service.default.importFromExcel.exec(_vm.fileList,"ImportSummery")
                    .done(function (read_response) {
                        window.location.reload();
                        _vm.flag = false;
                    })

            }
        }
     }

</script>