using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BiaoProject.Service.Helper;
using BiaoProject.Service.Voucher.Analytics;
using BiaoProject.Service.Voucher.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BiaoProject.Service.UnitTest
{
    [TestClass]
    public class ExcelHelperTest
    {
        [TestMethod]
        public void TestHelper()
        {
            ExcelHelper helper =new ExcelHelper();
            List<ColumnMapping> mappings = Voucher.DataAccess.VoucherColumnMapping.GetColumnMappings();

            var result = helper.GetAllFromCsv<Voucher.Models.Voucher>(@"Data\TestCsv.csv", mappings);
            Assert.IsTrue(result.Any());
            var group = result.GroupBy(v => Tuple.Create(v.PatNumber, v.VoucherServiceDate)).ToDictionary(e => e.Key,e=>e.ToList());
            foreach (var v in  group)
            {
                if (v.Value.Count > 1)
                {
                    
                    Debug.WriteLine(v.Key.Item1.ToString() + " " + v.Key.Item2.ToString());
                }
            }
            var dict = result.ToDictionary(v => Tuple.Create(v.PatNumber, v.VoucherServiceDate));
            Debug.WriteLine("All Vouchers Loaded, Count = {0}", result.Count);
        }
    }

    [TestClass]
    public class VoucherAnalyticTest
    {
        [TestMethod]
        public void TestAnalyticsGroupBy()
        {
            
            VoucherAnalytics an =new VoucherAnalytics(new VoucherService(GlobalCache.Instance));
            var result =an.GroupAllValidVisitsByRegion();
        }

        
    }

}
