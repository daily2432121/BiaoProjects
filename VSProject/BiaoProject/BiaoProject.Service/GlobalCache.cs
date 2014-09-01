using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BiaoProject.Service.Helper;
using BiaoProject.Service.Voucher.DataAccess;

namespace BiaoProject.Service
{

    public class GlobalCache:IVoucherRepository
    {
        private const int DEFAULTREFRESHINTERVAL = 20;
        // Singleton variables
        private static GlobalCache _instance = null;
        private static ManualResetEvent _initEvent = new ManualResetEvent(false);
        private static Object _lock = new object();

        public List<Voucher.Models.Voucher> Vouchers = new List<Voucher.Models.Voucher>(); 


        public Dictionary<Tuple<long,DateTime>, Voucher.Models.Voucher> VoucherDict = new Dictionary<Tuple<long,DateTime>, Voucher.Models.Voucher>();

        public static GlobalCache Instance
        {
            get
            {
                // might still be starting up for the first time
                if (_instance == null)
                    _initEvent.WaitOne();

                GlobalCache g = null;

                lock (_lock)
                {
                    g = _instance;
                }

                return g;
            }
        }


        static GlobalCache ()
        {
            //Read Test Mode config
            //if no config, do regular refresh.
            //if there is a config and set to true, skip regular refresh and the code is on Test/Manually Mode
            if (ConfigurationManager.AppSettings["EnableGlobalDataTestMode"] != null && ConfigurationManager.AppSettings["EnableGlobalDataTestMode"].ToLower() == "true")
            {
                return;
            }
            ThreadPool.QueueUserWorkItem(state => AutoRefresh());
            //ThreadPool.QueueUserWorkItem(state => AutoRefreshDiffCodes());
        }

        public static void AutoRefresh()
        {
            int updateInterval;
            string updateIntervalsetting = ConfigurationManager.AppSettings["GlobalDataRefreshInterval"];
            if (!Int32.TryParse(updateIntervalsetting, out updateInterval)) { updateInterval = DEFAULTREFRESHINTERVAL; }

            while (true)
            {
                GlobalCache g = Create();

                // first instance init
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        _instance = g;
                    }
                    _initEvent.Set();
                }
                else
                {
                    lock (_lock)
                    {
                        _instance = g;
                    }
                }

                Thread.Sleep(updateInterval * 1000);
            }
        }

        private static GlobalCache Create()
        {
            try
            {
                LogWriter.ToTrace(MethodInfo.GetCurrentMethod().Name, "Global Data refresh @ " + DateTime.Now.ToString());

                GlobalCache gd = new GlobalCache();
                gd.Load();
                gd.ToDictionary();

                LogWriter.ToTrace(MethodInfo.GetCurrentMethod().Name, "Global Data refresh complete: " + DateTime.Now.ToString());
                return gd;
            }
            catch (Exception e)
            {
                LogWriter.ToTrace("GlobalData.Create() Exception:", e.ToString());
                return null;
            }
        }

        private void ToDictionary()
        {
            VoucherDict = Vouchers.ToDictionary(v => Tuple.Create(v.PatNumber, v.VoucherServiceDate));
        }

        private void Load()
        {
            ExcelHelper helper =new ExcelHelper();
            Vouchers = helper.GetAllFromCsv<Voucher.Models.Voucher>("TestCsv.csv", VoucherColumnMapping.GetColumnMappings());

        }

        public RepositoryResult<string, List<Voucher.Models.Voucher>> GetAllVouchers()
        {
            RepositoryResult<string, List<Voucher.Models.Voucher>> result =new RepositoryResult<string, List<Voucher.Models.Voucher>>();
            if (!Vouchers.Any())
            {
                result.Item = null;
                result.ErrorMessage = "Failed To Get All Vouchers";
                result.Success = false;
                result.Key = null;
            }
            else
            {
                result.Item = Vouchers;
                result.ErrorMessage = null;
                result.Success = true;
                result.Key = null;
            }
            return result;
        }


        public RepositoryResult<Tuple<long,DateTime>, Voucher.Models.Voucher> GetAllVoucherByPatNumberAndVoucherServiceDate(long patNumber, DateTime serviceDate)
        {
            var key = Tuple.Create(patNumber, serviceDate);
            RepositoryResult<Tuple<long, DateTime>, Voucher.Models.Voucher> result = new RepositoryResult<Tuple<long, DateTime>, Voucher.Models.Voucher>();
            Voucher.Models.Voucher v;
            if (!VoucherDict.TryGetValue(key,out v))
            {
                result.Item = null;
                result.ErrorMessage =string.Format("Failed To Get Vouchers by key {0} & {1}",patNumber,serviceDate);
                result.Success = false;
                result.Key = null;
            }
            else
            {
                result.Item = v;
                result.ErrorMessage = null;
                result.Success = true;
                result.Key = key;
            }
            return result;
        }
    }
}
