using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace PrinterWebInterface.Pages
{
    public class LoggingModel : PageModel
    {
        public string Log => string.Join(Environment.NewLine, Logging.Log.GetLogMessages());

        public void OnGet()
        {
        }
    }
}
