<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DailyChart.aspx.cs" Inherits="BiaoProject.Mobile.DailyChart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
    <script type="text/javascript" >
        google.load('visualization', '1.0', { 'packages': ['corechart'] });
        google.setOnLoadCallback(drawChart);

        function drawChart() {

            var jsonData = $.ajax({
                url: "Analytics/Daily/ByRegions/Chart/Google",
                dataType: "string",
                async: false
            }).responseText;

            console.log("json!!");
            console.log(jsonData);

            var data = new google.visualization.DataTable(jsonData);
            

            console.log("data!!");
            console.log(data);


            var options = {
                width: 600,
                height: 400,
                legend: { position: 'top', maxLines: 3 },
                bar: { groupWidth: '100%' },
                isStacked: true,
            };

            var chart = new google.visualization.ColumnChart(document.getElementById('chart_div'));
            chart.draw(data, options);
        }


    </script>

    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="chart_div"></div>
    </div>
    </form>
</body>
</html>
