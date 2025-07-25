using Application.Services.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Persistence.Repositories.Abstractions;
using System.Security.Claims;

namespace Application.Features.Auth.Commands.Update
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdateUserResponseDto>
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;



        public UpdateUserCommandHandler(IUserService userService, IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }

        public async Task<UpdateUserResponseDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                throw new Exception("User not authorized!");

            var user = await _userRepository.GetByIdAsync(userId);

            if (user == null)
                throw new Exception("User not found");

            user.FullName = request.Request.FullName;

            await _userRepository.SaveChangesAsync();



            return new UpdateUserResponseDto
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName
            };
        }
    }
}
