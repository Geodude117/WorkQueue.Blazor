using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DomainData.Models.QuestionModels
{
    public class TextQuestion : IQuestion
    {
        public TextQuestion()
        {
        }

        public string Text { get ; set ; }
        public int Order { get ; set ; }
        public bool HasValidation { get ; set ; }

        [CustomValidation(typeof(TextQuestion), nameof(CustomRequired))]
        public string Value { get; set; }

        public int QuestionId { get; set; }

        public static ValidationResult CustomRequired(string value, ValidationContext vc)
        {
            // get has validtion property
            var containerType = vc.ObjectInstance.GetType();
            var field = containerType.GetProperty("HasValidation");
            bool checkForNull = false;
            ValidationResult result = ValidationResult.Success;

            if (field != null)
            {
                var extensionValue = field.GetValue(vc.ObjectInstance, null);
                checkForNull = (bool)extensionValue;
            }

            if (checkForNull)
            {
                if (string.IsNullOrEmpty(value))
                {
                    result = new ValidationResult($"The {vc.ObjectType.Name} field is required.", new[] { vc.ObjectType.Name });
                }
            }

            return result;

        }

    }
}
