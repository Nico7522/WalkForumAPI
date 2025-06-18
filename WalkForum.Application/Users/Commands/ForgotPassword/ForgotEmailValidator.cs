using FluentValidation;


namespace WalkForum.Application.Users.Commands.ForgotPassword;

public class ForgotEmailValidator: AbstractValidator<ForgotPasswordCommand>
{
    public ForgotEmailValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required").EmailAddress().WithMessage("Email format invalid");
    }
}
