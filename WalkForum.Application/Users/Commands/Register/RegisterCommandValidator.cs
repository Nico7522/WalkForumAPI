using FluentValidation;
using Microsoft.AspNetCore.Identity;
using WalkForum.Domain.Entities;

namespace WalkForum.Application.Users.Commands.Register;

internal class RegisterCommandValidator :AbstractValidator<RegisterCommand>
{
    UserManager<User> _userManager;
    public RegisterCommandValidator(UserManager<User> userManager)
    {
        this._userManager = userManager;
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Surname).NotEmpty();
        RuleFor(x => x.Password).Matches(@"^(?=.*[!@#=$%^&*(),.?"":{}|<>])(?=.*\d)(?=.*[A-Z]).{6,}$").WithMessage("Password not match the requirement").NotEmpty();
        RuleFor(x => x.PasswordConfirm).Equal(x => x.Password).WithMessage("Password and password confirmation are not equal");
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required").EmailAddress().WithMessage("Email format invalid");
        RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required");
    }
}
