using Application.Services.Abstractions;
using MediatR;
using Persistence.Repositories.Abstractions;

namespace Application.Features.Auth.Commands.ResetPassword
{
    public class ResetPasswordHandler : IRequestHandler<ResetPasswordCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public ResetPasswordHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        

        public async Task<bool> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByResetTokenAsync(request.Token);
            if (user == null || user.ResetTokenExpiry == null || user.ResetTokenExpiry < DateTime.UtcNow)
                return false;

            if (request.NewPassword != request.ConfirmPassword)
                return false;

            user.Password = request.NewPassword;
            user.ResetToken = null;
            user.ResetTokenExpiry = null;

            await _userRepository.SaveChangesAsync();

            return true;

        }
    }

}
