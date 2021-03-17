using System;
using System.Collections.Generic;
using System.Reflection;
using PrinterWebInterface.Utils;

namespace PrinterWebInterface.Logging.Syncs
{
    public class SystemdSync : ILoggerSync
    {
        public readonly string assemblyName = Assembly.GetExecutingAssembly().GetName().Name;

        public IEnumerable<string> GetLogMessages()
        {
            return CommandLine.ExecuteQuery($"journalctl --no-hostname --no-pager --unit=cups --unit={assemblyName}").Split(Environment.NewLine);
        }

        public void Error(string msg)
        {
            Console.WriteLine($"<3>{msg}");
        }

        public void Info(string msg)
        {
            Console.WriteLine($"<6>{msg}");
        }
    }
}
