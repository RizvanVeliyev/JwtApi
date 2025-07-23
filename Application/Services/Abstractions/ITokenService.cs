using Domain.Entities;

namespace Application.Services.Abstractions
{
    public interface ITokenService
    {
        string CreateToken(User user);
        RefreshToken GenerateRefreshToken();
    }
}
