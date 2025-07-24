using MediatR;

namespace Application.Features.Auth.Commands.ChangePassword
{
    public class ChangePasswordCommand : IRequest<bool>
    {
        public ChangePasswordRequestDto Request { get; set; }

        
    }

}
