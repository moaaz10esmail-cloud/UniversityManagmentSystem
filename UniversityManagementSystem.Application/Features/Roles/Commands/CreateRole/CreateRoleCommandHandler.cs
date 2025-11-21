using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using UniversityManagementSystem.Application.Services;
using UniversityManagementSystem.Core.Common;
using UniversityManagementSystem.Core.Entities.Identity;

namespace UniversityManagementSystem.Application.Features.Roles.Commands.CreateRole;

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Result<Guid>>
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly ICurrentUserService _currentUserService;
    private readonly ILogger<CreateRoleCommandHandler> _logger;

    public CreateRoleCommandHandler(
        RoleManager<ApplicationRole> roleManager,
        ICurrentUserService currentUserService,
        ILogger<CreateRoleCommandHandler> logger)
    {
        _roleManager = roleManager;
        _currentUserService = currentUserService;
        _logger = logger;
    }

    public async Task<Result<Guid>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var existingRole = await _roleManager.FindByNameAsync(request.Role.Name);
        if (existingRole != null)
        {
            return Result<Guid>.Failure("Role name already exists");
        }

        var role = new ApplicationRole
        {
            Name = request.Role.Name,
            Description = request.Role.Description,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = _currentUserService.UserId?.ToString() ?? "System"
        };

        var result = await _roleManager.CreateAsync(role);
        
        if (result.Succeeded)
        {
            _logger.LogInformation("Role {RoleName} created successfully by {UserId}", 
                request.Role.Name, _currentUserService.UserId);
                
            return Result<Guid>.Success(role.Id, "Role created successfully");
        }

        _logger.LogWarning("Failed to create role {RoleName}. Errors: {Errors}", 
            request.Role.Name, string.Join(", ", result.Errors.Select(e => e.Description)));
            
        return Result<Guid>.Failure(result.Errors.Select(e => e.Description).ToArray());
    }
}
