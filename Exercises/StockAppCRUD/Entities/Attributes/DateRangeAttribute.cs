using System.ComponentModel.DataAnnotations;

namespace Entities.Attributes
{
    public class DateRangeAttribute : ValidationAttribute
    {
        private readonly DateTime _minDate;

        public DateRangeAttribute(string minDate)
        {
            _minDate = DateTime.Parse(minDate);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dateTimeValue)
            {
                if (dateTimeValue < _minDate)
                {
                    return new ValidationResult($"Date and time of order cannot be older than {_minDate.ToShortDateString()}.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
