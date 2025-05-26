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

        public Task<EmployeeDto> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
        
        public Task UpdateAsync(EmployeeDto employeeDto)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsync(EmployeeDto employeeDto)
        {
            await _dbContext.Employees.AddAsync(employeeDto.MapToEmployeeEntity());
            await _dbContext.SaveChangesAsync();
        }

        public Task KillAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountActiveEmployeesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> CountInactiveEmployeesAsync()
        {
            throw new NotImplementedException();
        }

    }
}
