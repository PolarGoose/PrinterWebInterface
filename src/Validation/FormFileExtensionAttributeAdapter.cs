using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;

namespace PrinterWebInterface.Validation
{
    public class FormFileExtensionAttributeAdapter : AttributeAdapterBase<FormFileExtensionAttribute>
    {
        public FormFileExtensionAttributeAdapter(FormFileExtensionAttribute attribute, IStringLocalizer stringLocalizer)
            : base(attribute, stringLocalizer)
        {
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-formfileextension", GetErrorMessage(context));

            MergeAttribute(context.Attributes, "data-val-formfileextension-allowedextensions", string.Join(", ", Attribute.AllowedExtensions));
        }

        public override string GetErrorMessage(ModelValidationContextBase validationContext)
        {
            return GetErrorMessage(validationContext.ModelMetadata,
             validationContext.ModelMetadata.GetDisplayName());
        }
    }
}
