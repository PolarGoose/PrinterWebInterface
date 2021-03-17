using Microsoft.Extensions.Configuration;
using PrinterWebInterface.Logging;

namespace PrinterWebInterface.Printer
{
    public class DummyPrinter : IPrinter
    {
        private readonly string printer;
        public DummyPrinter(IConfiguration configuration)
        {
            printer = configuration["Printer"];
        }

        public void CancelAllJobs()
        {
            Log.Info($"CancelAllJobs is requested for {printer}");
        }

        public string GetStatus()
        {
            Log.Info($"GetStatus is requested for {printer}");
            return $"{printer} ready to print";
        }

        public void Print(string filePath, int numCopies, string pagesRange)
        {
            Log.Info($"Print is requested: filePath='{filePath}', numCopies='{numCopies}', pagesRange='{pagesRange}'");
        }
    }
}
