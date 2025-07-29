using MediatR;
using Persistence.Repositories.Abstractions;
using System.Security.Authentication;

namespace Application.Features.Auth.Commands.Logout
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public LogoutCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByRefreshTokenAsync(request.RefreshToken);
            if (user == null)
                throw new AuthenticationException("Invalid refresh token");

            // RefreshToken tək string olduğundan artıq list yoxdu
            if (user.RefreshRevoked != null)
                throw new AuthenticationException("Refresh token already revoked or invalid");

            user.RefreshRevoked = DateTime.UtcNow;

            await _userRepository.SaveChangesAsync();

            return true;
        }
    }
}
