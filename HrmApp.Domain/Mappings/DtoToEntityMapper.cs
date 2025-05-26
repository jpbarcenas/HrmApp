using HrmApp.Domain.Dtos;
using HrmApp.Domain.Entities;

namespace HrmApp.Domain.Mappings
{
    public static class DtoToEntityMapper
    {
        public static Employee MapToEmployeeEntity(this EmployeeDto employeeDto)
        {
            return new Employee
            {
                Id = employeeDto.Id,
                Name = employeeDto.Name,
                Email = employeeDto.Email,
                Department = employeeDto.Department,
                StartDate = employeeDto.StartDate,
                IsActive = employeeDto.IsActive,
            };
        }
    }
}
