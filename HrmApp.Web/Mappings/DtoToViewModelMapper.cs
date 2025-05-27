using HrmApp.Domain.Dtos;
using HrmApp.Web.Models;

namespace HrmApp.Web.Mappings
{
    public static class DtoToViewModelMapper
    {
        public static EmployeeViewModel MapToEmployeeViewModel(this EmployeeDto employeeDto)
        {
            return new EmployeeViewModel
            {
                Id = employeeDto.Id,
                Name = employeeDto.Name,
                Email = employeeDto.Email,
                Department = employeeDto.Department,
                StartDate = employeeDto.StartDate,
                IsActive = employeeDto.IsActive,
            };
        }

        public static AddOrUpdateEmployeeViewModel MapToAddOrUpdateEmployeeViewModel(this EmployeeDto employeeDto)
        {
            return new AddOrUpdateEmployeeViewModel
            {
                Id = employeeDto.Id,
                Name = employeeDto.Name,
                Email = employeeDto.Email,
                Department = employeeDto.Department,
                StartDate = employeeDto.StartDate,
                IsActive = employeeDto.IsActive,
            };
        }

        public static List<EmployeeViewModel> MapToEmployeeListViewModel(this IEnumerable<EmployeeDto> employeeListDto)
        {
            var employeeListViewModel = new List<EmployeeViewModel>();

            foreach (var employee in employeeListDto)
            {
                employeeListViewModel.Add(
                     new EmployeeViewModel
                     {
                         Id = employee.Id,
                         Name = employee.Name,
                         Email = employee.Email,
                         Department = employee.Department,
                         StartDate = employee.StartDate,
                         IsActive = employee.IsActive,
                     }
                );
            }

            return employeeListViewModel;
        }
    }
}
