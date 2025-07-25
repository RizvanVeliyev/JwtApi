using Application.Services.Abstractions;
using MediatR;

namespace Application.Features.Auth.Commands.ResetPassword
{
    public class ResetPasswordHandler : IRequestHandler<ResetPasswordCommand, bool>
    {
        private readonly IUserService _userService;

        public ResetPasswordHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<bool> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            return await _userService.ResetPasswordAsync(request.Token, request.NewPassword, request.ConfirmPassword);
        }
    }

}
