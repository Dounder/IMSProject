using FluentValidation;

namespace IMS.Application.UseCases.Users.Commands;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();

        RuleFor(x => x.UserName)
            .MinimumLength(3)
            .MaximumLength(50);

        RuleFor(x => x.Email)
            .EmailAddress();

        RuleForEach(x => x.Roles)
            .ChildRules(roles =>
            {
                roles.RuleFor(x => x.Name)
                    .NotEmpty()
                    .MaximumLength(50);

                roles.RuleFor(x => x.Id)
                    .NotEmpty();
            });
    }
}