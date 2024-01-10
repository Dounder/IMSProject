using IMS.Application.UseCases.Users.DTOs;
using IMS.Application.UseCases.Users.Services;
using IMS.Domain.Common;
using IMS.Domain.Entities;
using IMS.Domain.Interfaces;
using MediatR;

namespace IMS.Application.UseCases.Users.Queries;

public class GetUsersQueryHandler(IUnitOfWork repository, RoleService roleService)
    : IRequestHandler<GetUsersQuery, IEnumerable<UserDto>>
{
    public async Task<IEnumerable<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var filter = new FilterParams<User> { Pagination = request.Pagination };
        var users = await repository.UserRepository.GetAllAsyncMap<UserDto>(filter);

        users = users.Select(x =>
        {
            x.Roles = roleService.GetAllRoles<RoleDto>(x.Id).Result;
            return x;
        });

        return users;
    }
}