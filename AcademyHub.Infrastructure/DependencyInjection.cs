using AcademyHub.Application.Interfaces;
using AcademyHub.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace AcademyHub.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // Register as Singletons because they use static in-memory collections
            services.AddSingleton<IStudentService, InMemoryStudentService>();
            services.AddSingleton<IClassService, InMemoryClassService>();
            services.AddSingleton<IEnrollmentService, InMemoryEnrollmentService>();
            services.AddSingleton<IMarkService, InMemoryMarkService>();

            return services;
        }
    }
}
