using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BiaoProject.Service.Helper
{
    public class ColumnMapping
    {
        public string PropertyName { get; set; }
        public string ColumnName { get; set; }

        public ColumnMapping(string property, string column)
        {
            PropertyName = property;
            ColumnName = column;
        }
    }

    public static class Extensitions
    {
        public static string CalculateMD5(this string fileName)
        {
            try
            {
                using (var md5 = MD5.Create())
                {
                    using (var stream = File.OpenRead(fileName))
                    {
                        return BitConverter.ToString(md5.ComputeHash(stream));
                    }
                }
            }
            catch
            {
                return null;
            }
            
        }
    }
    

    public class ExcelHelper
    {



        public List<T> GetAllFromCsv<T>(string fileName, List<ColumnMapping> columnMapping  ) where T:new ()
        {
            var dict = columnMapping.ToDictionary(c => c.ColumnName.ToLower());
            List<T> result = new List<T>();
            using (StreamReader readFile = new StreamReader(fileName))
            {
                string line;
                

                string rawHeader = readFile.ReadLine();

                var headers = rawHeader.ToLower().Split(',').Select(e=>e.Trim().ToLower()).ToList();
                while ((line = readFile.ReadLine()) != null)
                {
                    T newItem = new T();
                    var row = line.Split(',');
                    for (int i = 0; i < row.Count(); i++)
                    {
                        string p = row[i].Trim();
                        var property = typeof(T).GetProperty(dict[headers[i]].PropertyName);
                        property.SetValue(newItem, Convert.ChangeType(p, property.PropertyType), null);
                    }
                    result.Add(newItem);
                }
            }
            return result;
        }
    }
}
