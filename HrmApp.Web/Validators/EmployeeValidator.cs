using System.ComponentModel.DataAnnotations;

namespace HrmApp.Web.Validators
{
    public class EmployeeValidator : ValidationAttribute
    {
        public static ValidationResult ValidateStartDate(DateTime startDate, ValidationContext context)
        {
            if (startDate > DateTime.Today)
            {
                return new ValidationResult("Start Date cannot be on the future.");
            }
            if (startDate < new DateTime(2000, 1, 1))
            {
                return new ValidationResult("Start Date cannot be before the year 2005");
            }
            return ValidationResult.Success;
        }

        public static ValidationResult ValidateCompanyEmail(string email, ValidationContext context)
        {
            if (!email.EndsWith("@company.com"))
            {
                return new ValidationResult("A @company.com email is required");
            }
            return ValidationResult.Success;
        }
    }
}
