using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AcademyHub.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            return services;
        }
    }
}
