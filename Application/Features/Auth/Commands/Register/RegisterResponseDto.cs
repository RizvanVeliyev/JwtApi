namespace Application.Features.Auth.Commands.Register
{
    public class RegisterResponseDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public bool TwoFactorEnabled { get; set; }
    }

}
