using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrinterWebInterface.Printer;

namespace PrinterWebInterface.Pages
{
    public partial class IndexModel : PageModel
    {
        private readonly string targetFilePath;
        private IPrinter printer;

        public IndexModel(IPrinter printer)
        {
            targetFilePath = Path.GetTempPath();
            this.printer = printer;
        }

        private string GetPrinterStatus()
        {
            return printer.GetStatus();
        }

        public void OnGet()
        {
            Input = new InputModel() { PrinterStatus = GetPrinterStatus() };
        }

        [BindProperty] public InputModel Input { get; set; }

        public IActionResult OnPostCancel()
        {
            printer.CancelAllJobs();
            Input.PrinterStatus = GetPrinterStatus();
            return Page();
        }

        public async Task<IActionResult> OnPostPrint()
        {
            if (ModelState.IsValid)
            {
                Logging.Log.Info($"printing {GetPrintInfo()}");
                await PrintFile();
            }
            else
            {
                Logging.Log.Error($"incorrect data {GetPrintInfo()}");
            }

            Input.PrinterStatus = GetPrinterStatus();
            return Page();
        }

        private async Task PrintFile()
        {
            var filePath = Path.Combine(targetFilePath, Guid.NewGuid() + Path.GetExtension(Input.FileUpload.FileName));
            using (var fileStream = System.IO.File.Create(filePath, Int16.MaxValue, FileOptions.DeleteOnClose))
            {
                await Input.FileUpload.CopyToAsync(fileStream);
                printer.Print(filePath, Input.NumCopies, Input.PrintRange);
            }
        }

        private string GetPrintInfo()
        {
            return $"file: {Input.FileUpload.FileName}, " +
                   $"num copies: {Input.NumCopies}, " +
                   $"pages range: {Input.PrintRange ?? "all"}";
        }
    }
}
