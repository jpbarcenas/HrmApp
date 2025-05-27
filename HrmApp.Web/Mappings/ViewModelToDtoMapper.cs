using HrmApp.Domain.Dtos;
using HrmApp.Web.Models;

namespace HrmApp.Web.Mappings
{
    public static class ViewModelToDtoMapper
    {
        public static EmployeeDto MapToEmployeeDto(this EmployeeViewModel employeeVm)
        {
            return new EmployeeDto
            {
                Id = employeeVm.Id,
                Name = employeeVm.Name,
                Email = employeeVm.Email,
                Department = employeeVm.Department,
                StartDate = employeeVm.StartDate,
                IsActive = employeeVm.IsActive,
            };
        }

        public static EmployeeDto MapToEmployeeDto(this AddOrUpdateEmployeeViewModel employeeVm)
        {
            return new EmployeeDto
            {
                Name = employeeVm.Name,
                Email = employeeVm.Email,
                Department = employeeVm.Department,
                StartDate = employeeVm.StartDate,
                IsActive = employeeVm.IsActive,
            };
        }
    }
}
