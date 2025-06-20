

using FluentValidation;

namespace WalkForum.Application.Users.Commands.ResetPassword;

public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordCommandValidator()
    {
        RuleFor(x => x.Password).Matches(@"^(?=.*[!@#=$%^&*(),.?"":{}|<>])(?=.*\d)(?=.*[A-Z]).{6,}$").WithMessage("Password not match the requirement").NotEmpty();
        RuleFor(x => x.PasswordConfirm).Equal(x => x.Password).WithMessage("Password and password confirmation are not equal");
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required").EmailAddress().WithMessage("Email format invalid");
        RuleFor(x => x.Token).NotEmpty().WithMessage("No token");

    }
}
