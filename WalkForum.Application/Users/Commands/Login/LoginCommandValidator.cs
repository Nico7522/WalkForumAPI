

using FluentValidation;

namespace WalkForum.Application.Users.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Password).Matches(@"^(?=.*[!@#=$%^&*(),.?"":{}|<>])(?=.*\d)(?=.*[A-Z]).{6,}$").WithMessage("Password not match the requirement").NotEmpty();
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required").EmailAddress().WithMessage("Email format invalid");
    }
}

