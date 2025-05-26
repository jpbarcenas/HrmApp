using HrmApp.Core.Implementations;
using HrmApp.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace HrmApp.Core
{
    public static class RepositoryLocator
    {
        public static IServiceCollection AddApplicationRepositories(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            return services;
        }
    }
}
