using HrmApp.Domain.Dtos;
using HrmApp.Domain.Entities;

namespace HrmApp.Domain.Mappings
{
    public static class EntityToDtoMapper
    {
        public static EmployeeDto MapToEmployeeDto(this Employee employee)
        {
            return new EmployeeDto
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                StartDate = employee.StartDate,
                IsActive = employee.IsActive,
            };
        }

        public static List<EmployeeDto> MapToEmployeeListDto(this IEnumerable<Employee> employeeList)
        {
            var employeeListDto = new List<EmployeeDto>();

            foreach (var employee in employeeList)
            {
                employeeListDto.Add(
                     new EmployeeDto
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

            return employeeListDto;
        }

    }
}
