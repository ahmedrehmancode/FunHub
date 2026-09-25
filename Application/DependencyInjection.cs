using Application.Common;
using Application.Common;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;       
using System.Reflection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<EmailConfig>(configuration.GetSection("EmailConfig"));
            services.Configure<AppBaseUrl>(configuration.GetSection("AppSettings"));
            

            // MediatR ko is all assembly handlers register
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

            // FluentValidation validators is assembly register
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            // FluentValidation validators Pipline
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }
}
