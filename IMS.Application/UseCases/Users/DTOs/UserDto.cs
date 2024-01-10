using IMS.Application.Common.DTOs;
using IMS.Domain.Entities;

namespace IMS.Application.UseCases.Users.DTOs;

public class UserDto : BaseDto
{
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public IEnumerable<RoleDto> Roles { get; set; } = null!;
}

public class RoleDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}