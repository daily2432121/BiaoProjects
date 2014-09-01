using System;
using System.Collections.Generic;
using System.Linq;
using BiaoProject.Service.Helper;
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

            var result = helper.GetAllFromCsv<Voucher.Models.Voucher>("TestCsv.csv", mappings);
            Assert.IsTrue(result.Any());
        }
    }
}
