using ServiceContracts.DTO;
using System.ComponentModel.DataAnnotations;

namespace Services.Helpers
{
    public class ValidationHelper
    {
        internal static void ModelValidation(object obj)
        {
            // Model validations
            ValidationContext validationContext = new ValidationContext(obj);
            List<ValidationResult> validationsResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(obj, validationContext, validationsResults, true);

            if (!isValid) throw new ArgumentException(validationsResults.FirstOrDefault()?.ErrorMessage);

        }
    }
}
