using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using BiaoProject.Mobile.ViewModels.Daily;

namespace BiaoProject.Mobile
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IVoucherMobileService" in both code and config file together.
    [ServiceContract]
    public interface IVoucherMobileService
    {

        [OperationContract]
        [WebGet(UriTemplate = "Analytics/Daily/ByRegions",RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        List<DailyVisitByRegion> GetDailyVisitByRegions();

        [OperationContract]
        [WebGet(UriTemplate = "Analytics/Daily/ByRegions/Chart", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        DailyVisitByDateStackAtRegionChart GetDailyVisitByRegionsChart();


        [OperationContract]
        [WebGet(UriTemplate = "Analytics/Daily/ByRegions/Chart/Google")]
        Stream GetDailyVisitByRegionsChartForGoogle_All();

        [OperationContract]
        [WebGet(UriTemplate = "Analytics/Daily/ByRegions/Chart/Google?startTime={startTime}&endTime={endTime}",ResponseFormat = WebMessageFormat.Json)]
        Stream GetDailyVisitByRegionsChartForGoogle(DateTime startTime, DateTime endTime);
        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
