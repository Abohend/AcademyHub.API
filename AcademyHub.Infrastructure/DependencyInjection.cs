using AcademyHub.Application.Interfaces;
using AcademyHub.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;

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

            //IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
            //{
            //    return HttpPolicyExtensions
            //        .HandleTransientHttpError()
            //        .CircuitBreakerAsync(
            //            handledEventsAllowedBeforeBreaking: 3,
            //            durationOfBreak: TimeSpan.FromSeconds(60),
            //            onBreak: (ex, breakDelay) =>
            //            {
            //                Console.WriteLine($"Circuit broken: {ex.Exception.Message}");
            //            },
            //            onReset: () =>
            //            {
            //                Console.WriteLine("Circuit reset");
            //            },
            //            onHalfOpen: () =>
            //            {
            //                Console.WriteLine("Circuit in half-open state");
            //            });
            //}

            return services;
        }
    }
}
