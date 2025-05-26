using HrmApp.Domain.Dtos;

namespace HrmApp.Core.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeDto>> FindAllAsync();
        Task<EmployeeDto> FindByIdAsync(int id);
        Task UpdateAsync(EmployeeDto employeeDto);
        Task CreateAsync(EmployeeDto employeeDto);
        Task KillAsync(int id);
        Task<int> CountActiveEmployeesAsync();
        Task<int> CountInactiveEmployeesAsync();
    }
}
