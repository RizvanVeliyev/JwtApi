using Application.Services.Abstractions;
using MediatR;

namespace Application.Features.Auth.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterResponseDto>
    {
        private readonly IUserService _userService;

        public RegisterCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        
        public async Task<RegisterResponseDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Request;

            var user = await _userService.RegisterAsync(dto);

            return new RegisterResponseDto
            {
                Id = user.Id,
                Email = user.Email,
                FullName=user.FullName


            };
        }
    }
}
