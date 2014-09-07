using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BiaoProject.Service;
using BiaoProject.Service.Helper;
using BiaoProject.Service.Voucher.DataAccess;
using BiaoProject.Service.Voucher.Models;

namespace BiaoProject.Mobile
{
    public partial class DailyChart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void UploadBtn_Click(object sender, EventArgs e)
        {
            if (fileUpload.HasFile && fileUpload.FileName.EndsWith(".csv"))
            {
                
                ExcelHelper helper =new ExcelHelper();
                using (fileUpload.FileContent)
                {
                    var checkResult = helper.CheckCsv<Voucher>(fileUpload.FileContent,VoucherColumnMapping.GetColumnMappings());
                    if (!checkResult.Success)
                    {
                        uploadMsg.Text = checkResult.ErrorMsg;
                        return;
                    }
                    
                }

                fileUpload.SaveAs(Server.MapPath("~/Data/Uploads/" + DateTime.UtcNow.ToLocalTime().ToString("yy-MM-dd_hh_mm_ss") + "_Uploaded_" + new Random().Next(1000) + ".csv"));
                GlobalCache.ForceRefresh();
                Response.Redirect(Request.RawUrl);

            }
            else
            {
                uploadMsg.Text = "Wrong file type. Please upload a .csv file";
            }
            return;
        }
    }
}