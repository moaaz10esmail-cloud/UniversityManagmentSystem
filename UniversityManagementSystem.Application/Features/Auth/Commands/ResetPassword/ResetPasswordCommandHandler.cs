using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Core.Common;
using UniversityManagementSystem.Core.Entities.Identity;

namespace UniversityManagementSystem.Application.Features.Auth.Commands.ResetPassword;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Result>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IApplicationDbContext _context;
    private readonly ILogger<ResetPasswordCommandHandler> _logger;

    public ResetPasswordCommandHandler(
        UserManager<ApplicationUser> userManager,
        IApplicationDbContext context,
        ILogger<ResetPasswordCommandHandler> logger)
    {
        _userManager = userManager;
        _context = context;
        _logger = logger;
    }

    public async Task<Result> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null || !user.IsActive)
        {
            _logger.LogWarning("Reset password attempt for non-existent or inactive user: {Email}", request.Email);
            return Result.Failure("Invalid reset attempt.");
        }

        // Reset password
        var result = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);
        
        if (result.Succeeded)
        {
            // Revoke all refresh tokens for security
            var userRefreshTokens = await _context.RefreshTokens
                .Where(rt => rt.UserId == user.Id && rt.Revoked == null)
                .ToListAsync(cancellationToken);

            foreach (var token in userRefreshTokens)
            {
                token.Revoked = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync(cancellationToken);
            
            _logger.LogInformation("Password reset successfully for user: {Email}", user.Email);
            return Result.Success("Password has been reset successfully.");
        }

        _logger.LogWarning("Failed to reset password for user: {Email}. Errors: {Errors}", 
            user.Email, string.Join(", ", result.Errors.Select(e => e.Description)));
            
        return Result.Failure(result.Errors.Select(e => e.Description).ToArray());
    }
}
