<template>
    <div>
        <div id='project_column'></div>
        <div id="monthly_pie"></div>
    </div>
</template>

<script>
    var echarts = require('echarts');
    var ecConfig = echarts.config;
    require('echarts/lib/chart/bar');
    require('echarts/lib/component/tooltip');
    require('echarts/lib/component/title');
    var formData_service = require('./../../services/form-data-service');
    export default {
        props: ['utilizationData', 'projectNo'],
        data() {
            return {
                columnData: [],
                columnxAxis: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
                pieData: [],
                pie: {}
            }
        },

        mounted() {
            var _vm=this;
            var myChart = echarts.init(document.getElementById("project_column"));
            this.pie = echarts.init(document.getElementById("monthly_pie"));

            this.columnxAxis.forEach((ele, index) => {
                if (this.getProperty(this.utilizationData, 'Month', ele)) {
                    this.columnData.push(this.getProperty(this.utilizationData, 'Month', ele).TotalWorkDays);
                }
                else {
                    this.columnData.push(0);
                }
            });

            this.drawPie(this.utilizationData[0].Month);

            myChart.setOption({
                title: {
                    text: 'Project No'+this.projectNo
                },
                tooltip: {},
                xAxis: {
                    data: this.columnxAxis
                },
                yAxis: {},
                series: [{
                    name: '销量',
                    type: 'bar',
                    data: this.columnData
                }]
            });

            myChart.on('click', 'series', function (params) {
                console.log(params.name);
                _vm.drawPie(params.name);
            })
        },
        created() {

        },
        methods: {
            getProperty(list, property, value) {
                for (let i in list) {
                    if (list[i][property] == value) {
                        return list[i];
                    }
                }

            },

            drawPie(month) {
                this.pieData=[];
                formData_service.default.getProjectMonthlyUtilization.exec(this.projectNo, month)
                .then(data => {
                    data.data.forEach(x => {
                        this.pieData.push({
                            value: x.WorkDays,
                            name: x.Name
                        })
                    });
                    var option = {
                    // backgroundColor: '#2c343c',

                    title: {
                        text: 'Project No'+this.projectNo,
                        left: 'center',
                        top: 20,
                        textStyle: {
                            color: '#ccc'
                        }
                    },

                    tooltip: {
                        trigger: 'item',
                        formatter: "{a} <br/>{b} : {c} ({d}%)"
                    },

                    visualMap: {
                        show: false,
                        min: 80,
                        max: 600,
                        inRange: {
                            colorLightness: [0, 1]
                        }
                    },
                    series: [
                        {
                            name: '',
                            type: 'pie',
                            radius: '55%',
                            center: ['50%', '50%'],
                            data: this.pieData.sort(function (a, b) { return (a.value - b.value) ; }),
                            roseType: 'radius',
                            label: {
                                normal: {
                                    textStyle: {
                                        color: 'rgba(255, 255, 255, 0.3)'
                                    }
                                }
                            },
                            labelLine: {
                                normal: {
                                    lineStyle: {
                                        color: 'rgba(255, 255, 255, 0.3)'
                                    },
                                    smooth: 0.2,
                                    length: 10,
                                    length2: 20
                                }
                            },
                            itemStyle: {
                                normal: {
                                    color: '#c23531',
                                    shadowBlur: 200,
                                    shadowColor: 'rgba(0, 0, 0, 0.5)'
                                }
                            },
                           
                            animationType: 'scale',
                            animationEasing: 'elasticOut',
                            animationDelay: function (idx) {
                                return Math.random() * 200;
                            }
                        }
                    ]
                };

                this.pie.setOption(option, true);
                });
                
            }
        },
    }

</script>