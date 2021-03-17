using System.Collections.Generic;
using PrinterWebInterface.Logging.Syncs;

namespace PrinterWebInterface.Logging
{
    public static class Log
    {
        private static ILoggerSync sync;

        public static void SetLogger(ILoggerSync s)
        {
            sync = s;
        }

        public static IEnumerable<string> GetLogMessages()
        {
            return sync.GetLogMessages();
        }

        public static void Info(string msg)
        {
            sync.Info(msg);
        }

        public static void Error(string msg)
        {
            sync.Error(msg);
        }
    }
}
