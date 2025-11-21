using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using UniversityManagementSystem.Application.Services;
using UniversityManagementSystem.Core.Common;
using UniversityManagementSystem.Core.Entities.Identity;

namespace UniversityManagementSystem.Application.Features.Users.Commands.DeleteUser;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ICurrentUserService _currentUserService;
    private readonly ILogger<DeleteUserCommandHandler> _logger;

    public DeleteUserCommandHandler(
        UserManager<ApplicationUser> userManager,
        ICurrentUserService currentUserService,
        ILogger<DeleteUserCommandHandler> logger)
    {
        _userManager = userManager;
        _currentUserService = currentUserService;
        _logger = logger;
    }

    public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService.UserId;
        
        // Prevent users from deleting themselves
        if (request.UserId == currentUserId)
        {
            return Result.Failure("You cannot delete your own account");
        }

        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        
        if (user == null)
        {
            return Result.Failure("User not found");
        }

        // Soft delete - deactivate the user
        user.IsActive = false;
        var result = await _userManager.UpdateAsync(user);
        
        if (result.Succeeded)
        {
            _logger.LogInformation("User {UserId} deactivated successfully", request.UserId);
            return Result.Success("User deactivated successfully");
        }

        _logger.LogWarning("Failed to deactivate user {UserId}. Errors: {Errors}", 
            request.UserId, string.Join(", ", result.Errors.Select(e => e.Description)));
            
        return Result.Failure(result.Errors.Select(e => e.Description).ToArray());
    }
}
