using System.Net;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using UniversityManagementSystem.Application.Services;
using UniversityManagementSystem.Core.Common;
using UniversityManagementSystem.Core.Entities.Identity;

namespace UniversityManagementSystem.Application.Features.Auth.Commands.ForgotPassword;

public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, Result>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IEmailService _emailService;
    private readonly ILogger<ForgotPasswordCommandHandler> _logger;

    public ForgotPasswordCommandHandler(
        UserManager<ApplicationUser> userManager,
        IEmailService emailService,
        ILogger<ForgotPasswordCommandHandler> logger)
    {
        _userManager = userManager;
        _emailService = emailService;
        _logger = logger;
    }

    public async Task<Result> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null || !user.IsActive)
        {
            // For security reasons, don't reveal that the user doesn't exist
            _logger.LogWarning("Forgot password attempt for non-existent email: {Email}", request.Email);
            return Result.Success("If the email exists, a password reset link has been sent.");
        }

        // Generate password reset token
        var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
        
        // In a real application, you would send this token via email
        var resetLink = $"https://yourapp.com/reset-password?token={WebUtility.UrlEncode(resetToken)}&email={WebUtility.UrlEncode(user.Email)}";

        // Send email
        try
        {
            await _emailService.SendPasswordResetEmailAsync(user.Email!, user.FirstName, resetLink);
            _logger.LogInformation("Password reset email sent to: {Email}", user.Email);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send password reset email to: {Email}", user.Email);
            return Result.Failure("Failed to send reset email. Please try again.");
        }

        return Result.Success("If the email exists, a password reset link has been sent.");
    }
}
