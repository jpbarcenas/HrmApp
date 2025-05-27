using System.ComponentModel.DataAnnotations;

namespace HrmApp.Web.Validators
{
    public class EmployeeValidator : ValidationAttribute
    {

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
