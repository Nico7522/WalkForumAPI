
using FluentValidation;

namespace WalkForum.Application.Posts.Commands.UpdatePost;

public class UpdatePostCommandValidator : AbstractValidator<UpdatePostCommand>
{

    public UpdatePostCommandValidator()
    {
        RuleFor(dto => dto.Title).Length(5, 50).WithMessage("Title's length doesn't match the requirements");
        RuleFor(dto => dto.Content).MinimumLength(2).NotEmpty().WithMessage("Content can't be empty");
    }
}
