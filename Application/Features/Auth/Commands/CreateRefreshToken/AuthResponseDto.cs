namespace Application.Features.Auth.Commands.CreateRefreshToken
{
    public class AuthResponseDto
    {
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }
}
