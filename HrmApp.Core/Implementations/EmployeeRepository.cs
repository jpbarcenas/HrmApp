using HrmApp.Core.Interfaces;
using HrmApp.Domain;
using HrmApp.Domain.Dtos;
using HrmApp.Domain.Mappings;
using Microsoft.EntityFrameworkCore;

namespace HrmApp.Core.Implementations
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly HrmAppDbContext _dbContext;

        public EmployeeRepository(HrmAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<EmployeeDto>> FindAllAsync()
        {
            var employees = await _dbContext.Employees.ToListAsync();

            return employees.MapToEmployeeListDto();
        }

        public async Task<EmployeeDto> FindByIdAsync(int id)
        {
            var employee = await _dbContext.Employees.FindAsync(id);

            return employee.MapToEmployeeDto();
        }
        
        public async Task UpdateAsync(EmployeeDto employeeDto)
        {
            _dbContext.Employees.Update(employeeDto.MapToEmployeeEntity());
            await _dbContext.SaveChangesAsync();
        }

        public async Task CreateAsync(EmployeeDto employeeDto)
        {
            await _dbContext.Employees.AddAsync(employeeDto.MapToEmployeeEntity());
            await _dbContext.SaveChangesAsync();
        }

        public async Task KillAsync(int id)
        {
            var employee = await _dbContext.Employees.FindAsync(id);

            if (employee != null)
            {
                _dbContext.Employees.Remove(employee);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<int> CountActiveEmployeesAsync()
        {
            return await _dbContext.Employees.CountAsync(e => e.IsActive);
        }

        public async Task<int> CountInactiveEmployeesAsync()
        {
            return await _dbContext.Employees.CountAsync(e => !e.IsActive);
        }

    }
}
