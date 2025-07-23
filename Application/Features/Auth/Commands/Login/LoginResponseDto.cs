using Domain.Entities;

namespace Application.Features.Auth.Commands.Login
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public RefreshToken RefreshToken { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
