using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementSystem.API.Controllers.Base;
using UniversityManagementSystem.Application.Features.Auth.Commands.ChangePassword;
using UniversityManagementSystem.Application.Features.Auth.Commands.ConfirmEmail;
using UniversityManagementSystem.Application.Features.Auth.Commands.ForgotPassword;
using UniversityManagementSystem.Application.Features.Auth.Commands.Login;
using UniversityManagementSystem.Application.Features.Auth.Commands.Logout;
using UniversityManagementSystem.Application.Features.Auth.Commands.RefreshToken;
using UniversityManagementSystem.Application.Features.Auth.Commands.Register;
using UniversityManagementSystem.Application.Features.Auth.Commands.ResendEmailConfirmation;
using UniversityManagementSystem.Application.Features.Auth.Commands.ResetPassword;

namespace UniversityManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : BaseApiController
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginCommand command)
    {
        var result = await Mediator.Send(command);
        return HandleResult(result);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterCommand command)
    {
        var result = await Mediator.Send(command);
        return HandleResult(result);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken(RefreshTokenCommand command)
    {
        var result = await Mediator.Send(command);
        return HandleResult(result);
    }

    [HttpPost("change-password")]
    [Authorize]
    public async Task<IActionResult> ChangePassword(ChangePasswordCommand command)
    {
        var result = await Mediator.Send(command);
        return HandleResult(result);
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordCommand command)
    {
        var result = await Mediator.Send(command);
        return HandleResult(result);
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(ResetPasswordCommand command)
    {
        var result = await Mediator.Send(command);
        return HandleResult(result);
    }

    [HttpPost("confirm-email")]
    public async Task<IActionResult> ConfirmEmail(ConfirmEmailCommand command)
    {
        var result = await Mediator.Send(command);
        return HandleResult(result);
    }

    [HttpPost("resend-email-confirmation")]
    public async Task<IActionResult> ResendEmailConfirmation(ResendEmailConfirmationCommand command)
    {
        var result = await Mediator.Send(command);
        return HandleResult(result);
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout(LogoutCommand command)
    {
        var result = await Mediator.Send(command);
        return HandleResult(result);
    }
}
