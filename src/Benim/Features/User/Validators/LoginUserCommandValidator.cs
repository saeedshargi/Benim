using Benim.Features.User.Commands;
using FluentValidation;

namespace Benim.Features.User.Validators;

public class LoginUserCommandValidator: AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(c => c.UserName)
            .NotEmpty()
            .WithMessage("User Name is required!");
        RuleFor(c => c.Password)
            .NotEmpty()
            .WithMessage("Password is required!");
        RuleFor(c => c.Password).Length(8, 15).WithMessage("Password length should be between 8 and 15");
        RuleFor(c => c.Password)
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$")
            .WithMessage("Password should contain character, number and special character");
    }
}