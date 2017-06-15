using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpCheckSvc
{
    class Helper
    {

        private EventLog log = new EventLog();
        public Helper()
        {
            string name = "InterpCheckSvc";
            log.Source = name;
            log.Log = "Application";
        }
        public void WriteToLog(string s)
        {
            string filename = @"C:\temp\InterpSvcLog.txt";
            System.IO.File.AppendAllText(filename, s + "\r\n");
            log.WriteEntry(s);
        }
    }
    
}
