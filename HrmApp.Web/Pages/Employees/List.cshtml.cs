using HrmApp.Core.Interfaces;
using HrmApp.Domain.Entities;
using HrmApp.Web.Mappings;
using HrmApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HrmApp.Web.Pages.Employees
{
    public class ListModel : PageModel
    {
        private readonly IEmployeeRepository _employeeRepository;

        public ListModel(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public List<EmployeeViewModel> Employees { get; set; }

        public async Task OnGetAsync()
        {
            var employees = await _employeeRepository.FindAllAsync();

            Employees = employees.MapToEmployeeListViewModel();
        }
    }
}
