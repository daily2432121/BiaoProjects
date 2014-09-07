using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using BiaoProject.Mobile.Chart;
using BiaoProject.Mobile.ViewModels.Daily;
using BiaoProject.Service;
using BiaoProject.Service.Voucher.Analytics;
using BiaoProject.Service.Voucher.Services;

namespace BiaoProject.Mobile
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "VoucherMobileService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select VoucherMobileService.svc or VoucherMobileService.svc.cs at the Solution Explorer and start debugging.
    public class VoucherMobileService : IVoucherMobileService
    {




        public VoucherMobileService()
        {
            string str = System.Web.Hosting.HostingEnvironment.MapPath("~/Data");
            GlobalCache.SetDataPath(str);
        }

        public List<DailyVisitByRegion> GetDailyVisitByRegions()
        {
         
            
            VoucherAnalytics an =new VoucherAnalytics(new VoucherService(GlobalCache.Instance));
            return DailyVisitByRegion.FromVoucher(an.GroupAllValidVisitsByRegion());
        }

        public DailyVisitByDateStackAtRegionChart GetDailyVisitByRegionsChart()
        {
            throw new NotImplementedException();
        }

        public DailyVisitByDateStackAtRegionChart GetDailyVisitByRegionsChart(DateTime startDate, DateTime endDate)
        {
            //TimeZoneInfo info=TimeZoneInfo.FindSystemTimeZoneById()
            //startDate = TimeZoneInfo.ConvertTimeFromUtc(startDate,TimeZoneInfo.Local);
            //endDate = TimeZoneInfo.ConvertTimeToUtc(endDate, TimeZoneInfo.Local);
            //startDate = startDate.ToLocalTime();
            //endDate = endDate.ToLocalTime();
            VoucherAnalytics an = new VoucherAnalytics(new VoucherService(GlobalCache.Instance));
            var result = an.GetAllValidVisitsRaw().Where(e=>e.VoucherServiceDate>=startDate && e.VoucherServiceDate<=endDate).ToList();
            var days = an.GroupAllByDateThenByRegion(startDate, endDate);
            List<string> regions = result.OrderBy(r => r.Location).Select(r => r.Location).Distinct().ToList();
            return new DailyVisitByDateStackAtRegionChart() {Count = days, Region = regions};
        }


        

        public Stream GetDailyVisitByRegionsChartForGoogle_All()
        {
            WebOperationContext.Current.OutgoingResponse.ContentType ="application/json; charset=utf-8";
            return new MemoryStream(Encoding.UTF8.GetBytes(GetDailyVisitByRegionsChart(DateTime.MinValue,DateTime.MaxValue).BuildArray()));
        }

        public Stream GetDailyVisitByRegionsChartForGoogle(DateTime startTime, DateTime endTime)
        {
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            return new MemoryStream(Encoding.UTF8.GetBytes(GetDailyVisitByRegionsChart(startTime,endTime).BuildArray()));
        }

        public Tuple<DateTime, DateTime> GetDailyVisitMinMaxDateTime()
        {
            VoucherAnalytics an = new VoucherAnalytics(new VoucherService(GlobalCache.Instance));
            var result = an.GetVoucherMinAndMaxServiceDate();
            return result;
        }
    }
}
