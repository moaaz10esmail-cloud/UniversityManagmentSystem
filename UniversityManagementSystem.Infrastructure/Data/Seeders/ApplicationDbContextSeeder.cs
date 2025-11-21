using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UniversityManagementSystem.Core.Entities.Academic;
using UniversityManagementSystem.Core.Entities.Identity;
using UniversityManagementSystem.Core.Enums;

namespace UniversityManagementSystem.Infrastructure.Data.Seeders;

public class ApplicationDbContextSeeder : IDatabaseSeeder
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly ILogger<ApplicationDbContextSeeder> _logger;

    public ApplicationDbContextSeeder(
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        ILogger<ApplicationDbContextSeeder> logger)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger;
    }

    public async Task SeedAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
            await SeedRolesAsync();
            await SeedAdminUserAsync();
            await SeedCollegesAndDepartmentsAsync();
            
            _logger.LogInformation("Database seeding completed successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database");
            throw;
        }
    }

    private async Task SeedRolesAsync()
    {
        foreach (var roleName in Enum.GetNames(typeof(UserType)))
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.CreateAsync(new ApplicationRole
                {
                    Name = roleName,
                    Description = $"{roleName} Role",
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "System"
                });
                _logger.LogInformation("Created role: {RoleName}", roleName);
            }
        }
    }

    private async Task SeedAdminUserAsync()
    {
        var adminEmail = "admin@university.com";
        var adminUser = await _userManager.FindByEmailAsync(adminEmail);
        
        if (adminUser == null)
        {
            adminUser = new ApplicationUser
            {
                UserName = "superadmin",
                Email = adminEmail,
                FirstName = "System",
                LastName = "Admin",
                UserType = UserType.SuperAdmin,
                EmailConfirmed = true,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(adminUser, "Admin123!");
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(adminUser, UserType.SuperAdmin.ToString());
                _logger.LogInformation("Created super admin user: {Email}", adminEmail);
            }
            else
            {
                _logger.LogError("Failed to create super admin user: {Errors}", 
                    string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }
    }

    private async Task SeedCollegesAndDepartmentsAsync()
    {
        if (!await _context.Colleges.AnyAsync())
        {
            var colleges = new[]
            {
                new College
                {
                    Name = "كلية علوم الحاسب والمعلومات",
                    Code = "CS",
                    Description = "كلية متخصصة في علوم الحاسب وتقنية المعلومات",
                    DeanName = "د. أحمد محمد",
                    ContactEmail = "cs@university.com",
                    IsActive = true,
                    CreatedBy = "System"
                },
                new College
                {
                    Name = "كلية الهندسة",
                    Code = "ENG",
                    Description = "كلية متخصصة في التخصصات الهندسية",
                    DeanName = "د. خالد عبدالله",
                    ContactEmail = "eng@university.com",
                    IsActive = true,
                    CreatedBy = "System"
                }
            };

            await _context.Colleges.AddRangeAsync(colleges);
            await _context.SaveChangesAsync();

            // Seed departments
            var csCollege = await _context.Colleges.FirstAsync(c => c.Code == "CS");
            var engCollege = await _context.Colleges.FirstAsync(c => c.Code == "ENG");

            var departments = new[]
            {
                new Department
                {
                    Name = "قسم علوم الحاسب",
                    Code = "CS",
                    Description = "قسم متخصص في علوم الحاسب الأساسية",
                    HeadOfDepartment = "د. محمد العلي",
                    CollegeId = csCollege.Id,
                    IsActive = true,
                    CreatedBy = "System"
                },
                new Department
                {
                    Name = "قسم تقنية المعلومات",
                    Code = "IT",
                    Description = "قسم متخصص في تقنية المعلومات",
                    HeadOfDepartment = "د. سارة أحمد",
                    CollegeId = csCollege.Id,
                    IsActive = true,
                    CreatedBy = "System"
                }
            };

            await _context.Departments.AddRangeAsync(departments);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Seeded colleges and departments");
        }
    }
}
