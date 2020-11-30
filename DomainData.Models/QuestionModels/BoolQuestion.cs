using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DomainData.Models.QuestionModels
{
    public class BoolQuestion : IQuestion
    {
        public BoolQuestion()
        {
        }
        public string Text { get ; set ; }
        public int Order { get ; set ; }
        public bool HasValidation { get ; set ; }

        [CustomValidation(typeof(BoolQuestion), nameof(CustomRequired))]
        public bool? Value { get; set; }
        public int QuestionId { get; set; }


        public static ValidationResult CustomRequired(bool? value, ValidationContext vc)
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
                if (value == null)
                {
                    result = new ValidationResult($"This {vc.ObjectType.Name} field is required.", new[] { vc.ObjectType.Name });
                }
            }



            return result;

        }

    }
}
