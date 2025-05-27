using HrmApp.Core.Interfaces;
using HrmApp.Web.Mappings;
using HrmApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace HrmApp.Web.Pages.Employees
{
    public class CreateModel : PageModel
    {
        private readonly IEmployeeRepository _employeeRepository;

        public CreateModel(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [BindProperty]
        public AddOrUpdateEmployeeViewModel Employee { get; set; } = default!;

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Employee.Id != 0)
            {
                ModelState.AddModelError($"Employee.Id", "Id must be empty");
            }

            var validationContext = new ValidationContext(Employee, null, null);
            var validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(Employee, validationContext, validationResults, true);

            if (!isValid)
            {
                foreach (var result in validationResults)
                {
                    foreach (var member in result.MemberNames)
                    {
                        ModelState.AddModelError($"Employee.{member}", result.ErrorMessage);
                    }
                }
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _employeeRepository.CreateAsync(Employee.MapToEmployeeDto());

            return RedirectToPage("./List");
        }
    }
}
