using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using UniversityManagementSystem.Application.Services;
using UniversityManagementSystem.Core.Common;
using UniversityManagementSystem.Core.Entities.Identity;

namespace UniversityManagementSystem.Application.Features.Roles.Commands.AssignRolesToUser;

public class AssignRolesToUserCommandHandler : IRequestHandler<AssignRolesToUserCommand, Result>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly ICurrentUserService _currentUserService;
    private readonly ILogger<AssignRolesToUserCommandHandler> _logger;

    public AssignRolesToUserCommandHandler(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        ICurrentUserService currentUserService,
        ILogger<AssignRolesToUserCommandHandler> logger)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _currentUserService = currentUserService;
        _logger = logger;
    }

    public async Task<Result> Handle(AssignRolesToUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user == null)
        {
            return Result.Failure("User not found");
        }

        // Get current user roles
        var currentUserRoles = await _userManager.GetRolesAsync(user);

        // Remove existing roles
        var removeResult = await _userManager.RemoveFromRolesAsync(user, currentUserRoles);
        if (!removeResult.Succeeded)
        {
            _logger.LogWarning("Failed to remove existing roles from user {UserId}. Errors: {Errors}", 
                request.UserId, string.Join(", ", removeResult.Errors.Select(e => e.Description)));
        }

        // Add new roles
        var addResult = await _userManager.AddToRolesAsync(user, request.Roles);
        if (addResult.Succeeded)
        {
            _logger.LogInformation("Roles {Roles} assigned to user {UserId} by {CurrentUserId}", 
                string.Join(", ", request.Roles), request.UserId, _currentUserService.UserId);
                
            return Result.Success("Roles assigned successfully");
        }

        _logger.LogWarning("Failed to assign roles to user {UserId}. Errors: {Errors}", 
            request.UserId, string.Join(", ", addResult.Errors.Select(e => e.Description)));
            
        return Result.Failure(addResult.Errors.Select(e => e.Description).ToArray());
    }
}
