using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using UniversityManagementSystem.Core.Common;
using UniversityManagementSystem.Core.Entities.Identity;

namespace UniversityManagementSystem.Application.Features.Auth.Commands.ConfirmEmail;

public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, Result>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<ConfirmEmailCommandHandler> _logger;

    public ConfirmEmailCommandHandler(
        UserManager<ApplicationUser> userManager,
        ILogger<ConfirmEmailCommandHandler> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<Result> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
        {
            _logger.LogWarning("Email confirmation attempt for non-existent user: {UserId}", request.UserId);
            return Result.Failure("Invalid confirmation attempt.");
        }

        if (user.EmailConfirmed)
        {
            return Result.Success("Email is already confirmed.");
        }

        var result = await _userManager.ConfirmEmailAsync(user, request.Token);
        
        if (result.Succeeded)
        {
            _logger.LogInformation("Email confirmed successfully for user: {Email}", user.Email);
            return Result.Success("Email confirmed successfully.");
        }

        _logger.LogWarning("Failed to confirm email for user: {Email}. Errors: {Errors}", 
            user.Email, string.Join(", ", result.Errors.Select(e => e.Description)));
            
        return Result.Failure(result.Errors.Select(e => e.Description).ToArray());
    }
}
