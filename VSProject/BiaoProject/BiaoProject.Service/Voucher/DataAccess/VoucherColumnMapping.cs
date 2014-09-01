using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiaoProject.Service.Helper;

namespace BiaoProject.Service.Voucher.DataAccess
{
    public static class VoucherColumnMapping
    {
        public static List<ColumnMapping> GetColumnMappings()
        {
            List<ColumnMapping> mappings = new List<ColumnMapping>()
            {
                new ColumnMapping("VoucherNumber","voucher number"),
                new ColumnMapping("UpdateStatus","update status"),
                new ColumnMapping("VoucherServiceDate","voucher service date"),
                new ColumnMapping("PatNumber","pat number"),
                new ColumnMapping("BillingCarrier","billing carrier"),
                new ColumnMapping("OriginalCarrier","original carrier"),
                new ColumnMapping("Department","department"),
                new ColumnMapping("PlaceOfService","place of service"),
                new ColumnMapping("Location","location"),
                new ColumnMapping("Fee","fee"),
                new ColumnMapping("PostedPayment","posted payment"),
                new ColumnMapping("PostedAdjustment","posted adjustment"),
                new ColumnMapping("PostedRefund","posted refund"),
                new ColumnMapping("PostedDebit","posted debit"),
                new ColumnMapping("VoucherBalance","voucher balance"),
            };
            return mappings;
        }
    }
}
