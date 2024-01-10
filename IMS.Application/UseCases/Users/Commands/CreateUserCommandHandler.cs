using AutoMapper;
using IMS.Application.UseCases.Auth.DTOs;
using IMS.Application.UseCases.Users.DTOs;
using IMS.Application.UseCases.Users.Services;
using IMS.Domain.Entities;
using IMS.Domain.Exceptions;
using IMS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace IMS.Application.UseCases.Users.Commands;

public class CreateUserCommandHandler(UserManager<User> userManager, IMapper mapper, RoleService roleService)
    : IRequestHandler<CreateUserCommand, UserDto>
{
    public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User { Email = request.Email, UserName = request.Username };
        var result = await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded) throw new BadRequestException("Failed to create user");

        await roleService.AddRoleToUser(user, request.Roles);

        var dto = mapper.Map<UserDto>(user);

        return dto;
    }
}