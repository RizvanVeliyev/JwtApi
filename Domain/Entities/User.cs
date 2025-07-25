using Domain.Enums;

namespace Domain.Entities
{
    public class User
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Email { get; set; }

        public string Password { get; set; } //hash

        public string FullName { get; set; }

        public List<RefreshToken> RefreshTokens { get; set; }
        public UserRole Role { get; set; }

        public string? ResetToken { get; set; }
        public DateTime? ResetTokenExpiry { get; set; }

    }
}
