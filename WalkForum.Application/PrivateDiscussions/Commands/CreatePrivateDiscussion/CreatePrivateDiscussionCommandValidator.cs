

using FluentValidation;

namespace WalkForum.Application.PrivateDiscussions.Commands.CreatePrivateDiscussion;

public class CreatePrivateDiscussionCommandValidator : AbstractValidator<CreatePrivateDiscussionCommand>
{
    public CreatePrivateDiscussionCommandValidator()
    {
        RuleFor(d => d.ReceiverId)
            .NotEmpty()
            .WithMessage("Receiver ID is required");
        RuleFor(d => d.Text).NotEmpty().WithMessage("Text is required")
            .MaximumLength(1000)
            .WithMessage("Text cannot exceed 1000 characters");
    }
}
