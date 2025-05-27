using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HrmApp.Web.Validators
{
    public class DatesValidator : ValidationAttribute
    {
        public static ValidationResult ValidatePastStartDate(DateTime startDate, ValidationContext context)
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
        public static ValidationResult ValidateWeekDayStartDate(DateTime startDate, ValidationContext context)
        {
            if (startDate.DayOfWeek == DayOfWeek.Saturday || startDate.DayOfWeek == DayOfWeek.Sunday)
            {
                return new ValidationResult("Invalid date because it falls on a weekend");
            }
            return ValidationResult.Success;
        }
    }
}
