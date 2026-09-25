using Application.Interface;
using Application.Interface.Repositories;
using Application.Interface.Servies;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Infrastructure.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices
            (this IServiceCollection services,
            IConfiguration configuration)
        {
            // Databse Configuration
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // UnitOfWork Register
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Service Register
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IContentRepository, ContentRepository>();

            // Repositries
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IEmailService, EmailService>();

            return services;
        }
    }
}
