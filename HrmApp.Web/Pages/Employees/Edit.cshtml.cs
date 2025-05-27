using HrmApp.Core.Interfaces;
using HrmApp.Web.Mappings;
using HrmApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HrmApp.Web.Pages.Employees
{
    public class EditModel : PageModel
    {
        private readonly IEmployeeRepository _employeeRepository;

        [BindProperty]
        public AddOrUpdateEmployeeViewModel Employee { get; set; } = default!;

        public EditModel(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {

            var employee = await _employeeRepository.FindByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            Employee = employee.MapToAddOrUpdateEmployeeViewModel();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Employee.Id == 0)
            {
                ModelState.AddModelError($"Employee.Id", "Id must not be empty");
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

            try
            {
                await _employeeRepository.UpdateAsync(Employee.MapToEmployeeDto());
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(Employee.Id).Result)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private async Task<bool> EmployeeExists(int id)
        {
            var employee = await _employeeRepository.FindByIdAsync(id);

            if (employee.Id != 0)
            {
                return true;
            }

            return false;
        }
    }
}
