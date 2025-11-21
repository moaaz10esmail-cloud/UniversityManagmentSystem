using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using UniversityManagementSystem.Core.Enums;
using UniversityManagementSystem.Infrastructure.Identity.Options;

namespace UniversityManagementSystem.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("JwtSettings");
        services.Configure<JwtSettings>(jwtSettings);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
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
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"]!))
            };

            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                    {
                        context.Response.Headers.Append("Token-Expired", "true");
                    }
                    return Task.CompletedTask;
                }
            };
        });

        services.AddAuthorization(options =>
        {
            // Policy for SuperAdmin only
            options.AddPolicy("SuperAdminOnly", policy => 
                policy.RequireRole(UserType.SuperAdmin.ToString()));

            // Policy for Admin and SuperAdmin
            options.AddPolicy("AdminOnly", policy => 
                policy.RequireRole(UserType.Admin.ToString(), UserType.SuperAdmin.ToString()));

            // Policy for Lecturers and above
            options.AddPolicy("StaffOnly", policy =>
                policy.RequireRole(
                    UserType.Lecturer.ToString(),
                    UserType.Admin.ToString(), 
                    UserType.SuperAdmin.ToString()));
        });

        return services;
    }
}
