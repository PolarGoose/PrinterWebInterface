using Microsoft.Extensions.Configuration;
using PrinterWebInterface.Utils;

namespace PrinterWebInterface.Printer
{
    public class LinuxPrinter : IPrinter
    {
        private readonly string PrinterName;

        public LinuxPrinter(IConfiguration configuration)
        {
            PrinterName = configuration["Printer"];
        }

        public void CancelAllJobs()
        {
            CommandLine.ExecuteCommand($"cancel {PrinterName}", ignoreExitCode: true);
        }

        public string GetStatus()
        {
            return CommandLine.ExecuteQuery($"lpstat -p {PrinterName}");
        }

        public void Print(string filePath, int numCopies, string pagesRange = null)
        {
            CommandLine.ExecuteCommand(pagesRange == null
                ? $"lp -d {PrinterName} -o media=A4 -n {numCopies} {filePath}"
                : $"lp -d {PrinterName} -o media=A4 -n {numCopies} -P {pagesRange} {filePath}");
        }
    }
}
