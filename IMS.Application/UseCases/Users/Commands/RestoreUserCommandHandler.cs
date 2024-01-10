using IMS.Application.UseCases.Users.Services;
using IMS.Domain.Entities;
using IMS.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace IMS.Application.UseCases.Users.Commands;

public class RestoreUserCommandHandler(UserManager<User> userManager, UserService userService) : IRequestHandler<RestoreUserCommand, Unit>
{
    public async Task<Unit> Handle(RestoreUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userService.GetByIdAsync(request.Id, withDeleted: true,cancellationToken: cancellationToken);

        if (user.DeletedAt is null) throw new BadRequestException("User is already active");

        user.DeletedAt = null;

        var result = await userManager.UpdateAsync(user);

        if (!result.Succeeded) throw new BadRequestException("Failed to restore user");

        return Unit.Value;
    }
}