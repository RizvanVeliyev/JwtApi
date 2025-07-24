using Domain.Enums;

namespace Application.Features.Auth.Queries.GetAllUsers
{
    public class UserResponseDto
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }
    }
}
