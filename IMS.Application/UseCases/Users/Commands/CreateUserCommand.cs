using IMS.Application.UseCases.Auth.DTOs;
using IMS.Application.UseCases.Users.DTOs;
using IMS.Domain.Enums;
using MediatR;

namespace IMS.Application.UseCases.Users.Commands;

public class CreateUserCommand : IRequest<UserDto>
{
    public string Username { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public List<string> Roles { get; set; } = [Role.User.ToString()];
}