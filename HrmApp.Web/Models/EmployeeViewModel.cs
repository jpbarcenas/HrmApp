using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using HrmApp.Web.Validators;

namespace HrmApp.Web.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Department { get; set; }

        public DateTime StartDate { get; set; }

        public bool IsActive { get; set; }
    }
}
