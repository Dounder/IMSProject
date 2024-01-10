using IMS.Application.UseCases.Users.DTOs;
using IMS.Domain.Common;
using MediatR;

namespace IMS.Application.UseCases.Users.Queries;

public class GetUsersQuery(PaginationParams pagination) : IRequest<IEnumerable<UserDto>>
{
    public PaginationParams Pagination { get; set; } = pagination;
}