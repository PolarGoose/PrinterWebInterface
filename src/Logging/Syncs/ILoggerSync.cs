using System.Collections.Generic;

namespace PrinterWebInterface.Logging.Syncs
{
    public interface ILoggerSync
    {
        void Info(string msg);

        void Error(string msg);

        IEnumerable<string> GetLogMessages();
    }
}

