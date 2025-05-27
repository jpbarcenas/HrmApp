using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using HrmApp.Web.Validators;

namespace HrmApp.Web.Models
{
    public class AddOrUpdateEmployeeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is mandatory")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
        [RegularExpression("^[^±!@£$%^&*_+§¡€#¢§¶•ªº«\\/<>?:;|=.,][0,9]{1,20}$", ErrorMessage = "Name contains invalid characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is mandatory")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [StringLength(100, ErrorMessage = "Email must not exceed 100 characters")]
        [CustomValidation(typeof(EmployeeValidator), "ValidateCompanyEmail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Department is mandatory")]
        [StringLength(50, ErrorMessage = "Department must not exceed 50 characters")]
        public string Department { get; set; }

        [Required(ErrorMessage = "Start Date is mandatory")]
        [DataType(DataType.Date)]
        [DisplayName("Start Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [CustomValidation(typeof(DatesValidator), "ValidatePastStartDate")]
        [CustomValidation(typeof(DatesValidator), "ValidateWeekDayStartDate")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Active is mandatory")]
        [DisplayName("Active")]
        public bool IsActive { get; set; }
    }
}
