using Application.Services.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistence.Repositories.Abstractions;

namespace Application.Features.Auth.Commands.ResetPassword
{
    public class ResetPasswordHandler : IRequestHandler<ResetPasswordCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<AppUser> _userManager;

        public ResetPasswordHandler(IUserRepository userRepository, UserManager<AppUser> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<bool> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByResetTokenAsync(request.Token);

            if (user == null || user.ResetTokenExpiry == null || user.ResetTokenExpiry < DateTime.UtcNow)
                return false;

            if (request.NewPassword != request.ConfirmPassword)
                return false;

            var removePassResult = await _userManager.RemovePasswordAsync(user);
            if (!removePassResult.Succeeded)
                return false;

            var addPassResult = await _userManager.AddPasswordAsync(user, request.NewPassword);
            if (!addPassResult.Succeeded)
                return false;

            user.ResetToken = null;
            user.ResetTokenExpiry = null;

            var updateResult = await _userManager.UpdateAsync(user);
            return updateResult.Succeeded;
        }
    }

}
