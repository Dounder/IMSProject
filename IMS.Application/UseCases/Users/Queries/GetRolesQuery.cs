using IMS.Application.UseCases.Users.DTOs;
using IMS.Domain.Common;
using MediatR;

namespace IMS.Application.UseCases.Users.Queries;

public class GetRolesQuery(PaginationParams paginationParams) : IRequest<List<RoleDto>>
{
    public PaginationParams PaginationParams { get; } = paginationParams;
}