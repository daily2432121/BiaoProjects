using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
}