using e_CommerceOrdersApp.Models;
using System.ComponentModel.DataAnnotations;

namespace e_CommerceOrdersApp.Validators
{
    public class PriceValidator : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var order = (Order)validationContext.ObjectInstance;

            if (order.Products == null || !order.Products.Any())
            {
                return new ValidationResult("The order must contain at least one product.");
            }

            var totalProductPrice = order.Products
                .Where(p => p != null)
                .Sum(p => p.Price * p.Quantity);

            if (order.InvoicePrice != totalProductPrice)
            {
                return new ValidationResult($"InvoicePrice ({order.InvoicePrice}) doesn't match with the total cost of the specified products in the order. ({totalProductPrice})");
            }

            return ValidationResult.Success;
        }
    }
}
