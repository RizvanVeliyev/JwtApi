using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;

        public string? ResetToken { get; set; }
        public DateTime? ResetTokenExpiry { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? RefreshRevoked { get; set; }
        public DateTime RefreshExpires { get; set; }
    }
}
