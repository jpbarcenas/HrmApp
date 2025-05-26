using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using HrmApp.Web.Validators;

namespace HrmApp.Web.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is mandatory")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is mandatory")]
        [EmailAddress(ErrorMessage = "Formato de correo electrónico inválido")]
        [StringLength(100, ErrorMessage = "El correo no puede exceder 100 caracteres")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Department is mandatory")]
        [StringLength(50, ErrorMessage = "El área no puede exceder 50 caracteres")]
        public string Department { get; set; }

        [Required(ErrorMessage = "Start Date is mandatory")]
        [DataType(DataType.Date)]
        [DisplayName("Start Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "IsActive is mandatory")]
        [DisplayName("Active")]
        public bool IsActive { get; set; }
    }
}
