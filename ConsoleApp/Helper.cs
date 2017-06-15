using System.Diagnostics;

namespace InterpConsoleApp
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
