using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiaoProject.Service.CheckUploads.Models
{
    public class FileStatus
    {
        public string FileName { get; set; }
        public string Path { get; set; }
        public DateTime UploadedTime { get; set; }
        public string MD5 { get; set; }
    }
}
