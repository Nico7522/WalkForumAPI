using FluentValidation;

namespace WalkForum.Application.UserProfile.Commands.UpdateUserProfileCommand;

public class UpdateUserProfileCommandValidator : AbstractValidator<UpdateUserProfileCommand>
{
    public UpdateUserProfileCommandValidator()
    {
    }
}
