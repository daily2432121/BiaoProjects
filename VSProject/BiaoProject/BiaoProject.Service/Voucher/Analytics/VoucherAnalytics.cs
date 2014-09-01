﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiaoProject.Service.Voucher.DataAccess;
using BiaoProject.Service.Voucher.Services;

namespace BiaoProject.Service.Voucher.Analytics
{
    public class VoucherAnalytics
    {
        private IVoucherService _service;
        

        public VoucherAnalytics(IVoucherService service)
        {
            _service = service;
        }

        public Dictionary<Tuple<long, DateTime>, Models.Voucher> GetAllValidVisits()
        {
            var vouchers = _service.GetAllVouchers();
            var result = vouchers.Where(e => e.Fee != 0 && e.UpdateStatus == 1).ToDictionary(e => Tuple.Create(e.PatNumber, e.VoucherServiceDate));
            return result;
        }

        public Dictionary<Tuple<DateTime, string>, int> GroupAllValidVisitsByRegion()
        {
            var vouchers = _service.GetAllVouchers();
            var result =
                vouchers.Where(e => e.Fee != 0 && e.UpdateStatus == 1)
                    .GroupBy(e => Tuple.Create(e.VoucherServiceDate, e.Location))
                    .ToDictionary(e => e.Key, e => e.Count());
            return result;
        }


    }

    public class DailyVisit
    {
        public Voucher.Models.Voucher Voucher { get; set; }
        public DateTime DateVisit { get; set; }

    }

}