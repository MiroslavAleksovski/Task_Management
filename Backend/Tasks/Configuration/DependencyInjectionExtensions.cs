using Services.Interfaces;
using Services.Implementations;
using AccessLevel.Interfaces;
using DataAccess.Implementations;
using DataAccess.Interfaces;

namespace Tasks.Configuration
{
    public static class DependencyInjectionExtensions
    {
        // Registers service-layer dependencies
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<IAuthService, AuthService>();
            return services;
        }

        // Registers repository-layer dependencies
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();

            return services;
        }
    }
}
