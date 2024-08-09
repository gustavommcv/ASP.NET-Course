using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ModelValidationExample.CustomValidators
{
    public class DataRangeValidatorAttribute : ValidationAttribute
    {
        public string OtherPropertyName { get; set; }

        public DataRangeValidatorAttribute() { }

        public DataRangeValidatorAttribute(string otherPropertyName) 
        { 
            OtherPropertyName = otherPropertyName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null) 
            {
                // get to_date
                var to_date = Convert.ToDateTime(value);

                // get from date
                PropertyInfo? otherProperty = validationContext.ObjectType.GetProperty(OtherPropertyName);
                var from_date = Convert.ToDateTime(otherProperty.GetValue(validationContext.ObjectInstance));
                if (otherProperty != null)
                {
                    if (from_date > to_date)
                    {
                        return new ValidationResult(ErrorMessage, new string[]
                        { OtherPropertyName, validationContext.MemberName });
                    }
                    return ValidationResult.Success;
                }
                return null;
            }
            return null;
        }
    }
}
