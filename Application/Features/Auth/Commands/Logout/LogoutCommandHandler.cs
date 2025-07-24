using Application.Services.Abstractions;
using MediatR;

namespace Application.Features.Auth.Commands.Logout
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand>
    {
        private readonly IUserService _userService;

        public LogoutCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Unit> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            await _userService.LogoutAsync(request.UserId);
            return Unit.Value;
        }
    }
}
