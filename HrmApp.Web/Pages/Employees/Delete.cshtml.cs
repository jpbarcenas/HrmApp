using HrmApp.Core.Interfaces;
using HrmApp.Domain.Entities;
using HrmApp.Web.Mappings;
using HrmApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HrmApp.Web.Pages.Employees
{
    public class DeleteModel : PageModel
    {
        private readonly IEmployeeRepository _employeeRepository;

        public DeleteModel(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [BindProperty]
        public EmployeeViewModel Employee { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {

                var employee = await _employeeRepository.FindByIdAsync(id);

                Employee = employee.MapToEmployeeViewModel();

                return Page();

            }
            catch (Exception ex)
            {
                return RedirectToPage("/Error", new { message = ex.Message, statusCode = 404, details = ex.InnerException });
            }
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            try
            {

                await _employeeRepository.KillAsync(id);

                return RedirectToPage("./List");

            }
            catch (Exception ex)
            {
                return RedirectToPage("/Error", new { message = ex.Message, statusCode = 500, details = ex.InnerException });
            }
        }
    }
}
