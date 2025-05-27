using HrmApp.Core.Interfaces;
using HrmApp.Domain.Entities;
using HrmApp.Web.Mappings;
using HrmApp.Web.Models;
using HrmApp.Web.Models.Pagination;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HrmApp.Web.Pages.Employees
{
    public class ListModel : PageModel
    {
        private readonly IEmployeeRepository _employeeRepository;
        private const int DefaultPageSize = 10;

        public ListModel(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public PagedResult<EmployeeViewModel> PagedEmployees { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = DefaultPageSize;

        //public List<EmployeeViewModel> Employees { get; set; }

        public async Task OnGetAsync()
        {
            var (employees, totalCount) = await _employeeRepository.FindAllAsync(CurrentPage, PageSize);

            PagedEmployees = new PagedResult<EmployeeViewModel>
            {
                Items = employees.MapToEmployeeListViewModel().ToList(),
                TotalCount = totalCount,
                PageNumber = CurrentPage,
                PageSize = PageSize
            };

            //var employees = await _employeeRepository.FindAllAsync();

            //Employees = employees.MapToEmployeeListViewModel();
        }
    }
}
