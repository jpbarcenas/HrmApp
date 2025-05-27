using HrmApp.Core.Exceptions;
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

        public async Task<(IEnumerable<EmployeeDto> employees, int totalCount)> FindAllAsync(
            int pageNumber = 1,
            int pageSize = 10,
            string sortBy = "Name",
            bool sortDescending = false)
        {
            try
            {
                var query = _dbContext.Employees.AsQueryable();

                query = sortBy switch
                {
                    "Name" => sortDescending ?
                        query.OrderByDescending(e => e.Name) :
                        query.OrderBy(e => e.Name),
                    "Email" => sortDescending ?
                        query.OrderByDescending(e => e.Email) :
                        query.OrderBy(e => e.Email),
                    "Department" => sortDescending ?
                        query.OrderByDescending(e => e.Department) :
                        query.OrderBy(e => e.Department),
                    "StartDate" => sortDescending ?
                        query.OrderByDescending(e => e.StartDate) :
                        query.OrderBy(e => e.StartDate),
                    _ => query.OrderBy(e => e.Name)
                };

                var totalCount = await query.CountAsync();

                var employees = await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return (employees.MapToEmployeeListDto(), totalCount);
            }
            catch (Exception ex)
            {
                throw new RecordNotFoundException("Failed to retrieve employees", ex);
            }
        }

        public async Task<EmployeeDto> FindByIdAsync(int id)
        {
            try
            {
                var employee = await _dbContext.Employees.FindAsync(id);

                if (employee == null)
                {
                    throw new RecordNotFoundException("No employees found with Id: " + id);
                }

                return employee.MapToEmployeeDto();
            }
            catch (Exception ex)
            {
                throw new RecordNotFoundException("Failed to retrieve employe", ex);
            }
        }
        
        public async Task UpdateAsync(EmployeeDto employeeDto)
        {
            try
            {
                _dbContext.Employees.Update(employeeDto.MapToEmployeeEntity());
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new RecordNotFoundException("Failed to update employee", ex);
            }
        }

        public async Task CreateAsync(EmployeeDto employeeDto)
        {
            try
            {
                await _dbContext.Employees.AddAsync(employeeDto.MapToEmployeeEntity());
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new RecordNotFoundException("Failed to create employee", ex);
            }
        }

        public async Task KillAsync(int id)
        {
            try
            {
                var employee = await _dbContext.Employees.FindAsync(id);

                if (employee == null)
                {
                    throw new RecordNotFoundException("No employees found with Id: " + id);
                }

                _dbContext.Employees.Remove(employee);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new RecordNotFoundException("Failed to delete employee", ex);
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
