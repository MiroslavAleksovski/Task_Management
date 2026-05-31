using Services.Interfaces;
using Services.Implementations;
using AccessLevel.Interfaces;
using AccessLevel.Implementations;

namespace Tasks.Configuration
{
    public static class DependencyInjectionExtensions
    {
        // Registers service-layer dependencies
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ITaskService, TaskService>();
            return services;
        }

        // Registers repository-layer dependencies
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITaskRepository, TaskRepository>();
            return services;
        }
    }
}
