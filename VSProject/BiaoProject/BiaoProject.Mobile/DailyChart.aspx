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
            var tableAsc = null;
            function getTotalRow(p) { // add a total row to the DataTable
                var row_count = p.getNumberOfRows();
                var column_count = p.getNumberOfColumns();
                var new_row = new Array();

                for (var col_i = 0; col_i < column_count; col_i++) { // total each column
                    var cell_value = 0;
                    if (col_i == 0) { // enter 'Total' on string columns
                        new_row[col_i] = "Total";
                    } else if (p.getColumnType(col_i) == 'number') {
                        // get column pattern
                        var col_pattern = p.getColumnPattern(col_i);

                        //if (col_pattern.match('%') != null) { // percent columns should not be totaled
                        //    new_row[col_i] = null;
                        //} else
                        { // total column values
                            for (row_i = 0; row_i < row_count; row_i++) {
                                cell_value = cell_value + p.getValue(row_i, col_i);
                            }
                            // create rounded value to 2 decimals
                            var cell_formatted = Math.round(cell_value * 100) / 100;

                            // no currency sign needed
                            new_row[col_i] = { v: cell_value, f: '' + cell_formatted };

                        }
                    } else { // boolean, data, datatime, timeofday types should not be
                        totaled
                        new_row[col_i] = null;
                    }
                }
                return new_row;
            }

            
            function transposeDataTable(dataTable) {
                //step 1: let us get what the columns would be
                var rows = [];//the row tip becomes the column header and the rest become
                for (var rowIdx = 0; rowIdx < dataTable.getNumberOfRows() ; rowIdx++) {
                    var rowData = [];
                    for (var colIdx = 0; colIdx < dataTable.getNumberOfColumns() ; colIdx++) {
                        rowData.push(dataTable.getValue(rowIdx, colIdx));
                    }
                    rows.push(rowData);
                }
                var newTB = new google.visualization.DataTable();
                newTB.addColumn('string', dataTable.getColumnLabel(0));
                newTB.addRows(dataTable.getNumberOfColumns() - 1);
                var colIdx = 1;
                for (var idx = 0; idx < (dataTable.getNumberOfColumns() - 1) ; idx++) {
                    var colLabel = dataTable.getColumnLabel(colIdx);
                    newTB.setValue(idx, 0, colLabel);
                    colIdx++;
                }
                for (var i = 0; i < rows.length; i++) {
                    var rowData = rows[i];
                    
                    newTB.addColumn('number', rowData[0]); //assuming the first one is always a header
                    var localRowIdx = 0;

                    for (var j = 1; j < rowData.length; j++) {
                        newTB.setValue(localRowIdx, (i + 1), rowData[j]);
                        localRowIdx++;
                    }
                }
                return newTB;
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
                        width: 1600,
                        height: 800,
                        legend: { position: top },
                    };
                    var chart = new google.visualization.ColumnChart(document.getElementById('chart_div'));
                    chart.draw(data, chartOptions);

                    
                    var table = new google.visualization.Table(document.getElementById('table_div'));
                    var newData = transposeDataTable(data);
                    var totalRow = getTotalRow(newData);
                    newData.addRow(totalRow);
                    table.draw(newData, tableOptions);
                    
                    
                    
                    google.visualization.events.addListener(table, 'sort',
                    function (e) {
                        newData.removeRow(newData.getNumberOfRows() - 1);
                        if (!tableAsc) {
                            tableAsc = e.ascending;
                        } else {
                            tableAsc = !tableAsc;
                        }
                        newData.sort([{ column: e.column, desc: tableAsc }]);
                        var sumRow = getTotalRow(newData);
                        newData.addRow(sumRow);
                        table.draw(newData, tableOptions);
                        
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
