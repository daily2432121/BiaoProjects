using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiaoProject.Service.Helper
{
    public static class LogWriter
    {
        public static void ToTrace(string methodName, string methodMessage, params string[] messageParameters)
        {
            Trace.WriteLine(methodName, string.Format(methodMessage,messageParameters) );
        }
    }
}
