<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DailyChart.aspx.cs" Inherits="BiaoProject.Mobile.DailyChart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
    <script type="text/javascript" >
        google.load('visualization', '1.0', { 'packages': ['corechart'] });
        $(document).ready(function() {

            var dataUrl = "Analytics/Daily/ByRegions/Chart/Google?startTime=2012-01-01T12:24:34&endTime=2012-02-29T08:15:30&callback=?";
            //var dataUrl = "http://VoucherChart.azurewebsites.net/Analytics/Daily/ByRegions/Chart/Google?startTime=2012-01-01T12:24:34&endTime=2012-02-29T08:15:30&callback=?";
            
            $.ajax({
                url: dataUrl,
                dataType: "jsonp",
                jsonp: "callback",
                jsonpCallback: "handler",
                success: function(d) {
                    var data = new google.visualization.DataTable(d);


                    var options = {
                        width: 1200,
                        height: 800,
                        legend: { position: 'top', maxLines: 3 },
                        bar: { groupWidth: '100%' },
                        isStacked: true,
                    };

                    var chart = new google.visualization.ColumnChart(document.getElementById('chart_div'));
                    chart.draw(data, options);
                }
            });


        });
    </script>

    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="chart_div">Waiting for data...</div>
    </div>
    </form>
</body>
</html>
