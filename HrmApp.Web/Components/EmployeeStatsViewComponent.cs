using HrmApp.Core.Interfaces;
using HrmApp.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace HrmApp.Web.Components
{
    public class EmployeeStatsViewComponent : ViewComponent
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeStatsViewComponent(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var activeCount = await _repository.CountActiveEmployeesAsync();
            var inactiveCount = await _repository.CountInactiveEmployeesAsync();

            return View(new EmployeeStatsViewModel
            {
                ActiveCount = activeCount,
                InactiveCount = inactiveCount
            });
        }
    }
}
