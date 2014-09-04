<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DailyChart.aspx.cs" Inherits="BiaoProject.Mobile.DailyChart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
    <script type="text/javascript" >
        google.load('visualization', '1.0', { 'packages': ['corechart','table'] });
        $(document).ready(function() {

            var dataUrl = "Analytics/Daily/ByRegions/Chart/Google?startTime=2012-01-01T12:24:34&endTime=2012-02-29T08:15:30&callback=?";
            //var dataUrl = "http://VoucherChart.azurewebsites.net/Analytics/Daily/ByRegions/Chart/Google?startTime=2012-01-01T12:24:34&endTime=2012-02-29T08:15:30&callback=?";

            
            function tryParseInt(str, defaultValue) {
                var retValue = defaultValue;
                if (str !== null) {
                    if (str.length > 0) {
                        if (!isNaN(str)) {
                            retValue = parseInt(str);
                        }
                    }
                }
                return retValue;
            }
            
            

            $.ajax({
                url: dataUrl,
                dataType: "jsonp",
                jsonp: "callback",
                jsonpCallback: "handler",
                success: function(d) {
                    var data = new google.visualization.DataTable(d);


                    var chartOptions = {
                        width: 1200,
                        height: 800,
                        domainAxis: { type: 'category' },
                        legend: { position: 'top', maxLines: 3 },
                        bar: { groupWidth: '100%' },
                        isStacked: true,
                    };
                    var tableOptions = {
                        width: 1200,
                        height: 800,
                        legend: { position: top },
                    };
                    var chart = new google.visualization.ColumnChart(document.getElementById('chart_div'));
                    chart.draw(data, chartOptions);


                    var table = new google.visualization.Table(document.getElementById('table_div'));
                    table.draw(data,tableOptions);

                    google.visualization.events.addListener(table, 'sort',
                    function (e) {
                        var view = new google.visualization.DataView(data);
                        view.setRows(data.getSortedRows({ column: e.column, desc: !e.ascending }));
                        //data.sort([{ column: e.column, desc: !e.ascending }]);
                        tableOptions.sortAscending = e.ascending;
                        tableOptions.sortColumn = e.column;
                        table.draw(view, tableOptions);
                        console.log(view);
                        chart.draw(view, chartOptions);
                    });
                }



            });
            

        });
    </script>

    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <p>
            <div>
                <div id="chart_div">Waiting for data...</div>
            </div>        
        </p>
        
        <p>
            <div>
                <div id="table_div">Waiting for data...</div>
            </div>    
        </p>
    </form>
</body>
</html>
