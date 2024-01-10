using AutoMapper;
using IMS.Application.UseCases.Users.DTOs;
using IMS.Application.UseCases.Users.Services;
using IMS.Domain.Exceptions;
using IMS.Domain.Interfaces;
using MediatR;

namespace IMS.Application.UseCases.Users.Queries;

public class GetUserByIdQueryHandler(IUnitOfWork repository, RoleService roleService) : IRequestHandler<GetUserByIdQuery, UserDto>
{
    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await repository.UserRepository.GetByIdAsyncMap<UserDto>(request.Id);

        if (user == null) throw new NotFoundException($"User not found");

        if (user.DeletedAt != null) throw new NotFoundException($"User inactive, please contact the administrator");

        user.Roles = await roleService.GetAllRoles<RoleDto>(user.Id);

        return user;
    }
}