using AutoMapper;
using IMS.Application.UseCases.Users.Services;
using IMS.Domain.Entities;
using IMS.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace IMS.Application.UseCases.Users.Commands;

public class UpdateUserCommandHandler(UserManager<User> userManager, IMapper mapper, UserService userService, RoleService roleService)
    : IRequestHandler<UpdateUserCommand, Unit>
{
    public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userService.GetByIdAsync(request.Id, cancellationToken: cancellationToken);

        user = mapper.Map(request, user);

        var result = await userManager.UpdateAsync(user);

        if (request.Roles is { Count: > 0 }) await roleService.UpdateRoles(user, request.Roles, cancellationToken);

        if (!result.Succeeded) throw new BadRequestException($"Failed to update user");

        return Unit.Value;
    }
}