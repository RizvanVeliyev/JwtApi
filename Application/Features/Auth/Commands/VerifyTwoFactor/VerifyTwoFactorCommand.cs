using Application.Features.Auth.Commands.Login;
using MediatR;

namespace Application.Features.Auth.Commands.VerifyTwoFactor
{
    public class VerifyTwoFactorCommand : IRequest<LoginResponseDto>
    {
        public VerifyTwoFactorRequestDto Request { get; set; }
    }

}
