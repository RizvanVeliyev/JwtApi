using Application.Services.Abstractions;
using MediatR;
using Persistence.Repositories.Abstractions;

namespace Application.Features.Auth.Commands.ForgotPassword
{
    public class ForgotPasswordHandler : IRequestHandler<ForgotPasswordCommand, string?>
    {
      
        private readonly IUserRepository _userRepository;


        public ForgotPasswordHandler( IUserRepository userRepository = null)
        {
            _userRepository = userRepository;
        }

        public async Task<string?> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null)
                return null;

            var token = Guid.NewGuid().ToString();
            user.ResetToken = token;
            user.ResetTokenExpiry = DateTime.UtcNow.AddMinutes(15);

            await _userRepository.SaveChangesAsync();
            var link = $"token={token}";


            return link;
        }
    }

}
