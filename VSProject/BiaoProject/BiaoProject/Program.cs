using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiaoProject.Service;
using BiaoProject.Service.Voucher.Analytics;
using BiaoProject.Service.Voucher.Services;

namespace BiaoProject
{
    class Program
    {
        static void Main(string[] args)
        {
            IVoucherService service = new VoucherService(GlobalCache.Instance);
            VoucherAnalytics analytics = new VoucherAnalytics(service);
            service.GetAllVouchers();
            Console.ReadKey();

        }
    }
}
