using MediatR;

namespace Application.Features.Auth.Commands.Login
{
    public class LoginCommand:IRequest<LoginResponseDto>
    {
        public LoginRequestDto Request { get; set; }
    }
}
