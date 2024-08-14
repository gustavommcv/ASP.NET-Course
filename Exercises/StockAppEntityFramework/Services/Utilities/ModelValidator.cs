using System.ComponentModel.DataAnnotations;

namespace Services.Utilities
{
    public class ModelValidator
    {
        public static void Validate(object model)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(model);

            bool isValid = Validator.TryValidateObject(model, validationContext, validationResults, true);

            if (!isValid)
            {
                var errorMessages = string.Join("; ", validationResults.ConvertAll(r => r.ErrorMessage));
                throw new ArgumentException(errorMessages);
            }
        }
    }
}
