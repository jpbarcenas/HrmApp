using HrmApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HrmApp.Domain
{
    public class HrmAppDbContext : DbContext
    {
        public HrmAppDbContext(DbContextOptions<HrmAppDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
