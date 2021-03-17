using System;
using System.Collections.Generic;

namespace PrinterWebInterface.Logging.Syncs
{
    public class ConsoleSync : ILoggerSync
    {
        private readonly List<string> messages = new();

        public IEnumerable<string> GetLogMessages()
        {
            return messages;
        }

        public void Error(string msg)
        {
            var message = $"Error: {msg}";

            messages.Add(message);
            Console.WriteLine(message);
        }

        public void Info(string msg)
        {
            messages.Add(msg);
            Console.WriteLine(msg);
        }
    }
}
