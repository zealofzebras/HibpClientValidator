using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace HibpClientValidator
{
    public class HibpAttribute : ValidationAttribute, IClientValidatable
    {

        public string CheckingMessage { get; set; }

        public HibpAttribute(string ErrorMessage)
        {
            this.ErrorMessage = ErrorMessage;
        }

        public override bool IsValid(object value)
        {
            return true;
        }


        public HibpAttribute(string ErrorMessage, string CheckingMessage)
        {
            this.ErrorMessage = ErrorMessage;
            this.CheckingMessage = CheckingMessage;
        }


        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = ErrorMessageString,
                ValidationType = "hibp"
            };

            rule.ValidationParameters["checking"] = CheckingMessage;

            yield return rule;
        }




    }
}
