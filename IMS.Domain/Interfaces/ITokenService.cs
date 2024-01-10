using IMS.Domain.Entities;

namespace IMS.Domain.Interfaces;

public interface ITokenService
{
    Task<string> GenerateAccessToken(User user);
    Task<string> GenerateRefreshToken(User user);
    Task<string> RenewAccessToken(string username, string refreshToken);
}