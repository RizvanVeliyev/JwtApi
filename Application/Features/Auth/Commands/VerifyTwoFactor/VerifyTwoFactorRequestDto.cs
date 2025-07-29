namespace Application.Features.Auth.Commands.VerifyTwoFactor
{
    public class VerifyTwoFactorRequestDto
    {
        public string Email { get; set; }
        public string TwoFactorCode { get; set; }
    }
}
