using Microsoft.AspNetCore.Identity;
using UniversityManagementSystem.API.Extensions;
using UniversityManagementSystem.Application.Extensions;
using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Application.Services;
using UniversityManagementSystem.Core.Entities.Identity;
using UniversityManagementSystem.Infrastructure.Data;
using UniversityManagementSystem.Infrastructure.Data.Seeders;
using UniversityManagementSystem.Infrastructure.Identity.Extensions;
using UniversityManagementSystem.Infrastructure.Identity.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add infrastructure services
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);

// Register Application DbContext interface
builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

// Configure Email Options
builder.Services.Configure<UniversityManagementSystem.Infrastructure.Services.Email.EmailOptions>(
    builder.Configuration.GetSection("EmailOptions"));

// Register Services
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IEmailService, UniversityManagementSystem.Infrastructure.Services.Email.MockEmailService>();

// Register Database Seeder
builder.Services.AddScoped<IDatabaseSeeder, ApplicationDbContextSeeder>();

// Add application services (includes MediatR, AutoMapper, FluentValidation, Behaviors, CurrentUserService, HttpContextAccessor)
builder.Services.AddApplicationServices();

// Add Swagger
builder.Services.AddSwaggerDocumentation();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .AllowAnyOrigin();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "University Management System API v1");
        options.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
    });
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Seed database
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<IDatabaseSeeder>();
    await seeder.SeedAsync();
}

app.Run();
