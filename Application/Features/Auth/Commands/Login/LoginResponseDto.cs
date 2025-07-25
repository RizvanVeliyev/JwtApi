using Domain.Entities;
using Domain.Enums;

namespace Application.Features.Auth.Commands.Login
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpiresAt { get; set; }
        public UserRole Role { get; set; }  
    }
}
