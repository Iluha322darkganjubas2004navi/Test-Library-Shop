using Library.Domain.Entities;

namespace Library.Infrastructure.Services.Interfaces;

public interface ITokenService
{
    string GenerateJwtToken(User user, int expirationMinutes = 30);
    string GenerateRefreshToken();
    string Hash(string inputString);
}