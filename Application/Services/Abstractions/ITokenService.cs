using Domain.Entities;

namespace Application.Services.Abstractions
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
        string GenerateRefreshToken();
    }
}
