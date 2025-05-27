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
            try
            {
                var employee = await _employeeRepository.FindByIdAsync(id);

                Employee = employee.MapToAddOrUpdateEmployeeViewModel();

                return Page();
            }
            catch (Exception ex)
            {
                return RedirectToPage("/Error", new { message = ex.Message, statusCode = 500, details = ex.InnerException });
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
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

                await _employeeRepository.UpdateAsync(Employee.MapToEmployeeDto());

                return RedirectToPage("./List");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return RedirectToPage("/Error", new { message = ex.Message, statusCode = 400, details = ex.InnerException });
            }
            catch (Exception ex)
            {
                return RedirectToPage("/Error", new { message = ex.Message, statusCode = 500, details = ex.InnerException });
            }
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
