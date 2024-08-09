using Microsoft.AspNetCore.Mvc.ModelBinding;
using ModelValidationExample.CustomValidators;
using System.ComponentModel.DataAnnotations;

namespace ModelValidationExample.Models
{
    public class Person
    {
        [Required(ErrorMessage = "{0} is not supplied")]
        [Display(Name = "Person Name")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "{0} should be between {2} and {1} characters")]
        [RegularExpression("^[A-Za-z .]*$", ErrorMessage = "{0} should contain only aphabets, space and dot (.)")]
        public string? PersonName { get; set; }

        [EmailAddress(ErrorMessage = "{0} should be a proper email address")]
        public string? Email { get; set; }

        [Phone(ErrorMessage = "{0} should be a proper phone number")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "{0} cant be blank")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "{0} cant be blank")]
        [Compare("Password", ErrorMessage = "{0} and {1} do not match")]
        public string? ConfirmPassword { get; set; }

        [Range(0, 999.99, ErrorMessage = "{0} should be between ${1} and ${2}")]
        public double? Price { get; set; }

        [MinimumYearValidator(2004, ErrorMessage = "Date of birth should not be newer than jan 01, {0}")]
        [BindNever]
        public DateTime? DateOfBirth { get; set; }

        public DateTime? FromDate { get; set; }

        [DataRangeValidator("FromDate", ErrorMessage = "'From Date' should be older than or equal to 'To Date'")]
        public DateTime? ToDate { get; set; }

        public override string ToString()
        {
            return $"Person name: {PersonName}\n" +
                $"Email: {Email}\n" +
                $"Phone: {Phone}\n" +
                $"Password: {Password}\n" +
                $"Confirm Password: {ConfirmPassword}\n" +
                $"Price: {Price}\n" +
                $"Date of Birth: {DateOfBirth}\n" +
                $"From Date: {FromDate}\n" +
                $"To Date: {ToDate}";
        }
    }
}
