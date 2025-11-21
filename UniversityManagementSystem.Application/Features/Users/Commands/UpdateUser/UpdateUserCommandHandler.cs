using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using UniversityManagementSystem.Core.Common;
using UniversityManagementSystem.Core.Entities.Identity;

namespace UniversityManagementSystem.Application.Features.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<UpdateUserCommandHandler> _logger;

    public UpdateUserCommandHandler(
        UserManager<ApplicationUser> userManager,
        ILogger<UpdateUserCommandHandler> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        
        if (user == null)
        {
            return Result.Failure("User not found");
        }

        // Update user properties
        user.FirstName = request.User.FirstName;
        user.LastName = request.User.LastName;
        user.NationalId = request.User.NationalId;
        user.DateOfBirth = request.User.DateOfBirth;
        user.Gender = request.User.Gender;
        user.IsActive = request.User.IsActive;

        var result = await _userManager.UpdateAsync(user);
        
        if (result.Succeeded)
        {
            _logger.LogInformation("User {UserId} updated successfully", request.UserId);
            return Result.Success("User updated successfully");
        }

        _logger.LogWarning("Failed to update user {UserId}. Errors: {Errors}", 
            request.UserId, string.Join(", ", result.Errors.Select(e => e.Description)));
            
        return Result.Failure(result.Errors.Select(e => e.Description).ToArray());
    }
}
