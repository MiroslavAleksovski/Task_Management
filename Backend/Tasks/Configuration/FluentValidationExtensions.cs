using FluentValidation;
using FluentValidation.AspNetCore;
using Tasks.Validations;

namespace Tasks.Configuration
{
    public static class FluentValidationExtensions
    {
        public static IServiceCollection AddFluentValidations(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();

            services.AddValidatorsFromAssemblyContaining<TaskAddUpdateDTOModelValidator>();
            return services;
        }
    }
}
