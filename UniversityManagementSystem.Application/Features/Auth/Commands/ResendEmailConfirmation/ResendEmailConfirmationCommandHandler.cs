using System.Net;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using UniversityManagementSystem.Application.Services;
using UniversityManagementSystem.Core.Common;
using UniversityManagementSystem.Core.Entities.Identity;

namespace UniversityManagementSystem.Application.Features.Auth.Commands.ResendEmailConfirmation;

public class ResendEmailConfirmationCommandHandler : IRequestHandler<ResendEmailConfirmationCommand, Result>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IEmailService _emailService;
    private readonly ILogger<ResendEmailConfirmationCommandHandler> _logger;

    public ResendEmailConfirmationCommandHandler(
        UserManager<ApplicationUser> userManager,
        IEmailService emailService,
        ILogger<ResendEmailConfirmationCommandHandler> logger)
    {
        _userManager = userManager;
        _emailService = emailService;
        _logger = logger;
    }

    public async Task<Result> Handle(ResendEmailConfirmationCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            // For security, don't reveal if user exists
            _logger.LogWarning("Resend confirmation attempt for non-existent email: {Email}", request.Email);
            return Result.Success("If the email exists, a confirmation link has been sent.");
        }

        if (user.EmailConfirmed)
        {
            return Result.Success("Email is already confirmed.");
        }

        var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var confirmationLink = $"https://yourapp.com/confirm-email?userId={user.Id}&token={WebUtility.UrlEncode(confirmationToken)}";

        try
        {
            await _emailService.SendEmailConfirmationEmailAsync(user.Email!, user.FirstName, confirmationLink);
            _logger.LogInformation("Confirmation email resent to: {Email}", user.Email);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to resend confirmation email to: {Email}", user.Email);
            return Result.Failure("Failed to send confirmation email. Please try again.");
        }

        return Result.Success("If the email exists, a confirmation link has been sent.");
    }
}
