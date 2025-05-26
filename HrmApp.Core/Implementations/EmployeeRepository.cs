using HrmApp.Core.Interfaces;
using HrmApp.Domain;
using HrmApp.Domain.Dtos;

namespace HrmApp.Core.Implementations
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly HrmAppDbContext _dbContext;

        public EmployeeRepository(HrmAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<EmployeeDto>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeDto> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
        
        public Task UpdateAsync(EmployeeDto employeeDto)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(EmployeeDto employeeDto)
        {
            throw new NotImplementedException();
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
