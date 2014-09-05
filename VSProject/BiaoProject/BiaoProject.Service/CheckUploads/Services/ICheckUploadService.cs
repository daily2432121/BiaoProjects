using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BiaoProject.Service.CheckUploads.Models;

namespace BiaoProject.Service.CheckUploads.Services
{
    public interface ICheckUploadService
    {
        List<FileStatus> GetUploadedFiles(DateTime since);
        FileStatus GetLastedUploadedFile(string path);
    }

    class CheckUploadService : ICheckUploadService
    {
        public List<FileStatus> GetUploadedFiles(DateTime since)
        {
            throw new NotImplementedException();
        }

        public FileStatus GetLastedUploadedFile(string path)
        {
            var dir= new DirectoryInfo(path);
            if (!dir.GetFiles().Any())
            {
                return null;
            }
            var last = dir.GetFiles().OrderByDescending(f => f.LastWriteTime).First();
            FileStatus fs = new FileStatus();
            fs.FileName = last.Name;
            fs.Path = path;
            fs.UploadedTime = last.CreationTime;
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(last.FullName))
                {
                    fs.MD5 = BitConverter.ToString(md5.ComputeHash(stream));
                }
            }
            return fs;
        }

    }
}
