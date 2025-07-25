using Application.Services.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Persistence.Repositories.Abstractions;
using System.Security.Claims;

namespace Application.Features.Auth.Commands.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, bool>
    {
        
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;


        public ChangePasswordCommandHandler( IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
           
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(userId))
                throw new UnauthorizedAccessException("User ID not found in token");

            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new Exception("User not found");

            var dto = request.Request;

            if (user.Password != dto.CurrentPassword)
                throw new Exception("Current password is incorrect");

            if (dto.NewPassword != dto.ConfirmPassword)
                throw new Exception("New passwords do not match");

            user.Password = dto.NewPassword;

            await _userRepository.SaveChangesAsync();

            return true;
        }
    }

}
