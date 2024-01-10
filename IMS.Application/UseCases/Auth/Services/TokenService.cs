using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using IMS.Domain.Entities;
using IMS.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace IMS.Application.UseCases.Auth.Services;

public class TokenService(IConfiguration configuration, UserManager<User> userManager) : ITokenService
{
    public async Task<string> GenerateAccessToken(User user)
    {
        var claims = new List<Claim> { new(ClaimTypes.Name, user.UserName!) };
        var roles = await userManager.GetRolesAsync(user);
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ?? throw new InvalidOperationException()));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddDays(1);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expires,
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var accessToken = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(accessToken);
    }

    public async Task<string> GenerateRefreshToken(User user)
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        var refreshToken = Convert.ToBase64String(randomNumber);

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7); // Set the lifespan of the refresh token
        await userManager.UpdateAsync(user);

        return refreshToken;
    }

    public async Task<string> RenewAccessToken(string username, string refreshToken)
    {
        var user = await userManager.FindByNameAsync(username);
        if (user == null) throw new UnauthorizedAccessException("Invalid user login");

        if (user.RefreshTokenExpiryTime < DateTime.UtcNow) throw new UnauthorizedAccessException("Refresh token expired, please login again");

        if (user.RefreshToken.Trim() != refreshToken.Trim()) throw new UnauthorizedAccessException("Invalid refresh token");

        return await GenerateAccessToken(user);
    }
}