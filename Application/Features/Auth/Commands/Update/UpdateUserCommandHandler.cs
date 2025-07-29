using Application.Services.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Persistence.Repositories.Abstractions;
using System.Security.Claims;

namespace Application.Features.Auth.Commands.Update
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdateUserResponseDto>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;
        private readonly UserManager<AppUser> _userManager;




        public UpdateUserCommandHandler(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository, UserManager<AppUser> userManager)
        {

            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<UpdateUserResponseDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                throw new Exception("User not authorized!");

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new Exception("User not found");

            user.Name = request.Request.FullName;

            await _userRepository.SaveChangesAsync();



            return new UpdateUserResponseDto
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.Name+user.Surname
            };
        }
    }
}
