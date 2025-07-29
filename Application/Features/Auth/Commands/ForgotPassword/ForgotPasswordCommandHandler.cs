using Application.Services.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistence.Repositories.Abstractions;

namespace Application.Features.Auth.Commands.ForgotPassword
{
    public class ForgotPasswordHandler : IRequestHandler<ForgotPasswordCommand, string?>
    {
      
        private readonly IUserRepository _userRepository;
        private readonly UserManager<AppUser> _userManager;



        public ForgotPasswordHandler(IUserRepository userRepository = null, UserManager<AppUser> userManager = null)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<string?> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return null;

            var token = Guid.NewGuid().ToString();
            user.ResetToken = token;
            user.ResetTokenExpiry = DateTime.UtcNow.AddMinutes(15);

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                return null;
            
            var link = $"token={token}";


            return link;
        }
    }

}
