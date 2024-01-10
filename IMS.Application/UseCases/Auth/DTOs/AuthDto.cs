using IMS.Application.UseCases.Users.DTOs;

namespace IMS.Application.UseCases.Auth.DTOs;

public class AuthDto
{
    public UserDto User { get; set; } = null!;
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
}

public class RenewTokenDto
{
    public string RefreshToken { get; set; } = null!;
}