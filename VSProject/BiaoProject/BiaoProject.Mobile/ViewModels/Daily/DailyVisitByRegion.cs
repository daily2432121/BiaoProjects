using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using BiaoProject.Mobile.Chart;
using Google.DataTable.Net.Wrapper;

namespace BiaoProject.Mobile.ViewModels.Daily
{
    public class DailyVisitByRegion
    {
        public int Count { get; set; }
        public string Region { get; set; }
        public DateTime DateTime { get; set; }

        public static List<DailyVisitByRegion> FromVoucher(Dictionary<Tuple<DateTime, string>, int> source)
        {
            List<DailyVisitByRegion> result = new List<DailyVisitByRegion>();
            foreach (var s in source)
            {
                DailyVisitByRegion d = new DailyVisitByRegion();
                d.DateTime = s.Key.Item1;
                d.Region = s.Key.Item2;
                d.Count = s.Value;
                result.Add(d);
            }

            return result;
        }


    }

    public class DailyVisitByDateStackAtRegionChart
    {
        public List<string> Region{ get; set; }
        public Dictionary<DateTime, Dictionary<string,int>> Count { get; set; }

        public string BuildArray()
        {

            var annotation = Region.OrderBy(r => r).ToList();
            var regions = Region.OrderBy(r => r).ToList();
            var dt = new Google.DataTable.Net.Wrapper.DataTable();
            dt.AddColumn(new Column(ColumnType.String, "Region"));
            var cols = annotation.Select(o=>new Column(ColumnType.Number,o.ToString(),o.ToString())).ToList();
            cols.ForEach(e=>dt.AddColumn(e));
            
            foreach (var k in Count.Sort())
            {
                Row r = dt.NewRow();
                r.AddCell(new Cell(k.Key.ToShortDateString()));
                for (int i = 0; i < regions.Count; i++)
                {
                    int c = 0;
                    if (k.Value.TryGetValue(regions[i], out c))
                    {
                        r.AddCell(new Cell(c));
                    }
                    else
                    {
                        r.AddCell(new Cell(0));
                    }
                }
                
                dt.AddRow(r);

            }
            string str = dt.GetJson();

            string pass = System.Web.HttpContext.Current.Request.QueryString["callback"];
            if (string.IsNullOrEmpty(pass) == false)
            {

                str = string.Format("{0}({1})", pass, str);
            }

            return str;

        }
    }

    
}