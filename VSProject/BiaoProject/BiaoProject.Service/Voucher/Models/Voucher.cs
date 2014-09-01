using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiaoProject.Service.Voucher.Models
{
    public class Voucher
    {
        public long VoucherNumber { get; set; }
        public int UpdateStatus { get; set; }
        public DateTime VoucherServiceDate { get; set; }
        public long PatNumber { get; set; }
        public string BillingCarrier { get; set; }
        public string OriginalCarrier { get; set; }
        public string Department { get; set; }
        public string PlaceOfService { get; set; }
        public string Location { get; set; }
        public decimal Fee { get; set; }
        public decimal PostedPayment { get; set; }
        public decimal PostedAdjustment { get; set; }
        public decimal PostedRefund { get; set; }
        public decimal PostedDebit { get; set; }
        public decimal VoucherBalance { get; set; }

    }
}
