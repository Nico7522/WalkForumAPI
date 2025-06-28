

using FluentValidation;

namespace WalkForum.Application.PrivateMessages.Commands.CreatePrivateMessage;

public class CreatePrivateMessageCommandValidator : AbstractValidator<CreatePrivateMessageCommand>
{
    public CreatePrivateMessageCommandValidator()
    {
        RuleFor(d => d.PrivateDiscussionId)
            .NotEmpty()
            .WithMessage("Private discussion ID is required");
        RuleFor(d => d.Text).NotEmpty()
            .WithMessage("Text is required")
            .MaximumLength(1000)
            .WithMessage("Text cannot exceed 1000 characters");
    }
}
