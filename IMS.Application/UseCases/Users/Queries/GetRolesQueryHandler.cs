using IMS.Application.UseCases.Users.DTOs;
using IMS.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace IMS.Application.UseCases.Users.Queries;

public class GetRolesQueryHandler(RoleManager<UserRole> roleManager) : IRequestHandler<GetRolesQuery, List<RoleDto>>
{
    public Task<List<RoleDto>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = roleManager.Roles.Select(r => new RoleDto
        {
            Id = r.Id,
            Name = r.Name ?? string.Empty,
        }).Skip(request.PaginationParams.Offset).Take(request.PaginationParams.Limit).ToList();

        return Task.FromResult(roles);
    }
}