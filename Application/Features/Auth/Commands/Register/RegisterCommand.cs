using MediatR;

namespace Application.Features.Auth.Commands.Register
{
    public class RegisterCommand:IRequest<RegisterResponseDto>
    {
        public RegisterRequestDto Request { get; set; }
        


    }
}
