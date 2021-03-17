using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PrinterWebInterface.Pages
{
    public partial class IndexModel
    {
        public class InputModel
        {
            [BindProperty]
            [FormFileExtension(".pdf", ErrorMessage = "Only .pdf files are allowed")]
            [Required]
            public IFormFile FileUpload { get; set; }

            [BindProperty]
            [Required]
            [Range(1, double.PositiveInfinity)]
            public int NumCopies { get; set; } = 1;

            [BindProperty]
            [RegularExpression(@"^((\d+)|(\d+-\d+))((,\d+)|(,\d+-\d+))*$")]
            public string PrintRange { get; set; }

            [BindProperty(SupportsGet = true)]
            public string PrinterStatus { get; set; }
        }
    }
}
