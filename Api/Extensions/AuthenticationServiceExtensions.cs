using Application.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Api.Extensions
{
    public static class AuthenticationServiceExtensions
    {
        public static IServiceCollection AddJwtAuthentication
            (this IServiceCollection services,
            IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["Secret"];

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings["Issuer"],
                        ValidAudience = jwtSettings["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(secretKey!)),
                        ClockSkew = TimeSpan.Zero
                    };

                    // Custom Error Response
                    options.Events = new JwtBearerEvents
                    {
                        OnChallenge = async context =>
                        {
                            // Default error band
                            context.HandleResponse();

                            // Apna custom response
                            context.Response.StatusCode = 401;
                            context.Response.ContentType = "application/json";

                            var response = ApiResponse<object>.UnauthorizedResponse();

                            await context.Response.WriteAsJsonAsync(response);
                        },


                        OnForbidden = async context =>
                        {
                            context.Response.StatusCode = 403;
                            context.Response.ContentType = "application/json";

                            var response = ApiResponse<object>.ForbiddenResponse();
                            await context.Response.WriteAsJsonAsync(response);
                        }
                    };
                });

            services.AddAuthorization();

            return services;
        }
    }
}