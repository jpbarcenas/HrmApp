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

        public async Task<IActionResult> OnPostAsync(int id)
        {
            await _employeeRepository.KillAsync(id);

            return RedirectToPage("./List");
        }
    }
}
