using MediatR;
using Microsoft.AspNetCore.Identity;
using UniversityManagementSystem.Application.Common.Interfaces;
using UniversityManagementSystem.Core.Common;
using UniversityManagementSystem.Core.Entities;

namespace UniversityManagementSystem.Application.Features.Auth.Commands.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICurrentUserService _currentUserService;

        public ChangePasswordCommandHandler(
            UserManager<ApplicationUser> userManager,
            ICurrentUserService currentUserService)
        {
            _userManager = userManager;
            _currentUserService = currentUserService;
        }

        public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(_currentUserService.UserId.ToString()!);
            if (user == null)
                return Result.Failure("User not found");

            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            
            if (result.Succeeded)
                return Result.Success();

            return Result.Failure(result.Errors.Select(e => e.Description).ToArray());
        }
    }
}
