using FluentValidation;
using IMS.Domain.Enums;

namespace IMS.Application.UseCases.Users.Commands;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
        
        RuleFor(x => x.Username)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(20);
        
        RuleFor(x => x.Roles)
            .Must(r => r.All(role => !string.IsNullOrEmpty(role)))
            .WithMessage("Role names cannot be empty.")
            .Must(r => r.All(role => Enum.IsDefined(typeof(Role), role)))
            .WithMessage("One or more roles are invalid.");
        
        // Validation for password with at least one uppercase letter, one lowercase letter, one number, and one special character
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
            .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"\d").WithMessage("Password must contain at least one digit.")
            .Matches(@"[#\$^+=!*()@%&]").WithMessage("Password must contain at least one special character.");
    }
}