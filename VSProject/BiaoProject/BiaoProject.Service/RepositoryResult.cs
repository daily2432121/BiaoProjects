using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiaoProject.Service
{
    public class RepositoryResult<T, V>
    {
        public T Key { get; set; }
        public V Item { get; set; }
        public string ErrorMessage { get; set; }
        public bool Success { get; set; }
    }
}
