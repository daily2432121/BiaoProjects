using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BiaoProject.Mobile.Chart
{
    public class GoogleChart
    {
        // Fields
        private string _data = "";
        private string _javascript;

        // Properties
        public string ElementId { get; set; }

        public int Height { get; set; }

        public string Title { get; set; }

        public int Width { get; set; }

        // ChartTypes
        public enum ChartType
        {
            BarChart,
            PieChart,
            LineChart,
            ColumnChart,
            AreaChart,
            BubbleChart,
            CandlestickChart,
            ComboChart,
            GeoChart,
            ScatterChart,
            SteppedAreaChart,
            TableChart
        }

        // Methods
        public GoogleChart()
        {
            this.Title = "Google Chart";
            this.Width = 730;
            this.Height = 300;
            this.ElementId = "chart_div";
        }

        public void AddColumn(string type, string columnName)
        {
            string data = this._data;
            this._data = data + "data.addColumn('" + type + "', '" + columnName + "');";
        }

        public void AddRow(string value)
        {
            this._data = this._data + "data.addRow([" + value + "]);";
        }

        public string GenerateChart(ChartType chart)
        {
            this._javascript = "<script type=\"text/javascript\" src=\"https://www.google.com/jsapi\"></script>";
            this._javascript = this._javascript + "<script type=\"text/javascript\">";
            this._javascript = this._javascript + "google.load('visualization', '1.0', { 'packages': ['corechart']});";
            this._javascript = this._javascript + "google.setOnLoadCallback(drawChart);";
            this._javascript = this._javascript + "function drawChart() {";
            this._javascript = this._javascript + "var data = new google.visualization.DataTable();";
            this._javascript = this._javascript + this._data;
            this._javascript = this._javascript + "var options = {";
            this._javascript = this._javascript + "'title': '" + this.Title + "',";
            object javascript = this._javascript;
            this._javascript = string.Concat(new object[] { javascript, "'width': ", this.Width, ", 'height': ", this.Height, "};" });
            string str = this._javascript;
            this._javascript = str + "var chart = new google.visualization." + chart.ToString() + "(document.getElementById('" + this.ElementId + "'));";
            this._javascript = this._javascript + "chart.draw(data, options);";
            this._javascript = this._javascript + "} </script>";
            return this._javascript;
        }
    }
}