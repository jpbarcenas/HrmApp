using HrmApp.Core.Interfaces;
using HrmApp.Web.Mappings;
using HrmApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HrmApp.Web.Pages.Employees
{
    public class DetailsModel : PageModel
    {
        private readonly IEmployeeRepository _employeeRepository;

        public DetailsModel(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public EmployeeViewModel Employee { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                var employee = await _employeeRepository.FindByIdAsync(id);
                if (employee == null)
                {
                    return NotFound();
                }
                else
                {
                    Employee = employee.MapToEmployeeViewModel();
                }
                return Page();

            }
            catch (Exception ex)
            {
                return RedirectToPage("/Error", new { message = ex.Message, statusCode = 500, details = ex.InnerException });
            }
        }
    }
}
