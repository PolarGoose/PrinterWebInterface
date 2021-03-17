using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PrinterWebInterface
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class FormFileExtensionAttribute : ValidationAttribute
    {
        public List<string> AllowedExtensions { get; set; }

        public FormFileExtensionAttribute(string fileExtensions)
        {
            AllowedExtensions = fileExtensions.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            AllowedExtensions.ForEach(a => a = a.Trim());
        }

        public override bool IsValid(object value)
        {
            if (value is IFormFile file)
            {
                var fileName = file.FileName;

                return AllowedExtensions.Any(e => fileName.EndsWith(e));
            }

            return true;
        }
    }
}
