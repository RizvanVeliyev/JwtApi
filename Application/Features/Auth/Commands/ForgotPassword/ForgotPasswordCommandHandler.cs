using Application.Services.Abstractions;
using MediatR;

namespace Application.Features.Auth.Commands.ForgotPassword
{
    public class ForgotPasswordHandler : IRequestHandler<ForgotPasswordCommand, string?>
    {
        private readonly IUserService _userService;

        public ForgotPasswordHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<string?> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            return await _userService.ForgotPasswordAsync(request.Email);
        }
    }

}
