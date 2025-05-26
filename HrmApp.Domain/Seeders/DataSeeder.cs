using HrmApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HrmApp.Domain.Seeders
{
    public static class DataSeeder
    {
        public static async Task SeedEmployeesAsync(HrmAppDbContext dbContext, int total)
        {
            if (await dbContext.Employees.AnyAsync())
            {
                return;
            }

            var firstNames = new[] { "John", "Emily", "Michael", "Sarah", "Robert", "Jennifer",
                                    "William", "Jessica", "David", "Amanda", "James", "Linda",
                                    "Christopher", "Michelle", "Daniel" };
            var lastNames = new[] { "Smith", "Johnson", "Brown", "Davis", "Wilson", "Martinez",
                                    "Anderson", "Taylor", "Thomas", "Garcia", "Rodriguez", "Martinez",
                                    "Lee", "Harris", "Clark" };
            var departments = new[] { "IT", "HR", "Finance", "Marketing", "Operations", "Sales" };
            var random = new Random();

            var employees = new List<Employee>();
            var usedEmails = new HashSet<string>();

            while (employees.Count < total)
            {
                var firstName = firstNames[random.Next(firstNames.Length)];
                var lastName = lastNames[random.Next(lastNames.Length)] + " " + lastNames[random.Next(lastNames.Length)];

                var email = firstName.ToLower() + "." + lastName.ToLower().Replace(" ", "") + "@company.com";

                // Handling email duplicates by adding a number suffix
                int counter = 1;
                while (usedEmails.Contains(email))
                {
                    email = $"{firstName.ToLower()}.{lastName.ToLower().Replace(" ", "")}{counter}@company.com";
                    counter++;
                }

                var isActive = random.Next(10) > 2;

                employees.Add(new Employee
                {
                    Name = $"{firstName} {lastName}",
                    Email = email,
                    Department = departments[random.Next(departments.Length)],
                    StartDate = DateTime.Now.AddYears(-random.Next(1, 5)),
                    IsActive = isActive
                });

                usedEmails.Add(email);
            }

            await dbContext.Employees.AddRangeAsync(employees);
            await dbContext.SaveChangesAsync();
        }
    }
}
