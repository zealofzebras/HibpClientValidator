
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.DataAnnotations;

namespace HibpClientValidator
{
    public class HibpAttribute : ValidationAttribute, IClientModelValidator
    {
        private bool _checkedForLocalizer;
        private IStringLocalizer _stringLocalizer;

        public string CheckingMessage { get; set; }

        public HibpAttribute(string ErrorMessage)
        {
            this.ErrorMessage = ErrorMessage;
        }

        public HibpAttribute(string ErrorMessage, string CheckingMessage)
        {
            this.ErrorMessage = ErrorMessage;
            this.CheckingMessage = CheckingMessage;
        }


        public void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            CheckForLocalizer(context);
            var errorMessage = GetErrorMessage(context.ModelMetadata.GetDisplayName());
            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-hibp", errorMessage);
            MergeAttribute(context.Attributes, "data-val-hibp-checking", CheckingMessage);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return ValidationResult.Success;
        }

        private static bool MergeAttribute(IDictionary<string, string> attributes, string key, string value)
        {
            if (attributes.ContainsKey(key))
            {
                return false;
            }
            attributes.Add(key, value);
            return true;
        }

        private void CheckForLocalizer(ClientModelValidationContext context)
        {
            if (!_checkedForLocalizer)
            {
                _checkedForLocalizer = true;

                var services = context.ActionContext.HttpContext.RequestServices;
                var options = services.GetRequiredService<IOptions<MvcDataAnnotationsLocalizationOptions>>();
                var factory = services.GetService<IStringLocalizerFactory>();

                var provider = options.Value.DataAnnotationLocalizerProvider;
                if (factory != null && provider != null)
                {
                    _stringLocalizer = provider(
                        context.ModelMetadata.ContainerType ?? context.ModelMetadata.ModelType,
                        factory);
                }
            }
        }

        private string GetErrorMessage(string displayName)
        {
            if (_stringLocalizer != null &&
                !string.IsNullOrEmpty(ErrorMessage) &&
                string.IsNullOrEmpty(ErrorMessageResourceName) &&
                ErrorMessageResourceType == null)
            {
                return _stringLocalizer[ErrorMessage, displayName];
            }

            return FormatErrorMessage(displayName);
        }

    }
}
