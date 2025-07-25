using Application.Services.Abstractions;
using Domain.Entities;
using MediatR;
using Persistence.Repositories.Abstractions;

namespace Application.Features.Auth.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterResponseDto>
    {
        private readonly IUserRepository _userRepository;

        public RegisterCommandHandler( IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public async Task<RegisterResponseDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Request;

            var exists = await _userRepository.AnyByEmailAsync(dto.Email);
            if (exists)
                throw new Exception("User already exists");

            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Email = dto.Email,
                Password = dto.Password,
                FullName = dto.FullName,
                Role = dto.Role
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();


            return new RegisterResponseDto
            {
                Id = user.Id,
                Email = user.Email,
                FullName=user.FullName

                //automapper yazacam
            };
        }
    }
}
