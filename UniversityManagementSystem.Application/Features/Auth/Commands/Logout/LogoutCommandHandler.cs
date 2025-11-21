using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Application.Services;
using UniversityManagementSystem.Core.Common;

namespace UniversityManagementSystem.Application.Features.Auth.Commands.Logout;

public class LogoutCommandHandler : IRequestHandler<LogoutCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly ILogger<LogoutCommandHandler> _logger;

    public LogoutCommandHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUserService,
        ILogger<LogoutCommandHandler> logger)
    {
        _context = context;
        _currentUserService = currentUserService;
        _logger = logger;
    }

    public async Task<Result> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        if (userId == null)
        {
            return Result.Failure("User not authenticated.");
        }

        // Find the specific refresh token to revoke
        var refreshToken = await _context.RefreshTokens
            .FirstOrDefaultAsync(rt => 
                rt.Token == request.RefreshToken && 
                rt.UserId == userId.Value && 
                rt.Revoked == null, 
                cancellationToken);

        if (refreshToken != null)
        {
            refreshToken.Revoked = DateTime.UtcNow;
            _context.RefreshTokens.Update(refreshToken);
            await _context.SaveChangesAsync(cancellationToken);
            
            _logger.LogInformation("User {UserId} logged out successfully", userId);
            return Result.Success("Logged out successfully.");
        }

        // If token not found, still return success for security
        _logger.LogWarning("Refresh token not found for logout: {Token}", request.RefreshToken);
        return Result.Success("Logged out successfully.");
    }
}
