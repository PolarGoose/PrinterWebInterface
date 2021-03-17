using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;

namespace PrinterWebInterface.Validation
{
    public class CustomValidatiomAttributeAdapterProvider : IValidationAttributeAdapterProvider
    {
        readonly IValidationAttributeAdapterProvider baseProvider = new ValidationAttributeAdapterProvider();
        public IAttributeAdapter GetAttributeAdapter(ValidationAttribute attribute, IStringLocalizer stringLocalizer)
        {
            if (attribute is FormFileExtensionAttribute)
            {
                return new FormFileExtensionAttributeAdapter(attribute as FormFileExtensionAttribute, stringLocalizer);
            }

            return baseProvider.GetAttributeAdapter(attribute, stringLocalizer);
        }
    }
}
