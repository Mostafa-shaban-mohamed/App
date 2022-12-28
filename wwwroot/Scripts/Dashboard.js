$(document).ready(function () {
    $.ajax({
        url: "/api/User/PieChart",
        type: 'GET',
        dataType: 'json', // added data type
        success: function (res) {
            var chart = new CanvasJS.Chart("PieChartContainer", {
                animationEnabled: true,
                title: {
                    text: "Users to Languages Percentage"
                },
                data: [{
                    type: "pie",
                    startAngle: 240,
                    yValueFormatString: "##0",
                    indexLabel: "{label} {y}",
                    dataPoints: res
                }]
            });
            chart.render();
        }
    });
    $.ajax({
        url: "/api/User/LineChart",
        type: 'GET',
        dataType: 'json', // added data type
        success: function (res) {
            var chart = new CanvasJS.Chart("LineChartContainer", {
                animationEnabled: true,
                theme: "light2",
                title:{
                    text: "Users and Age Relation"
                },
                axisX: {
                    title: "Age",
                    interval: 1,
                    minimum: 18
                },
                axisY: {
                    title: "No. of Users"
                },
                data: [{        
                    type: "line",
                    indexLabelFontSize: 16,
                    dataPoints: res
                }]
            });
            chart.render();
        }
    });
    
    
});