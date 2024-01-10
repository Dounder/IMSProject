using IMS.Application.UseCases.Users.DTOs;
using MediatR;

namespace IMS.Application.UseCases.Users.Commands;

public class UpdateUserCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public List<RoleDto>? Roles { get; set; }
}