

using FluentValidation;

namespace WalkForum.Application.PrivateMessages.Commands.UpdatePrivateMessage;

public class UpdatePrivateMessageCommandValidator : AbstractValidator<UpdatePrivateMessageCommand>
{
    public UpdatePrivateMessageCommandValidator()
    {
        RuleFor(d => d.PrivateDiscussionId)
            .NotEmpty()
            .WithMessage("Private discussion ID is required");
        RuleFor(d => d.PrivateMessageId)
            .NotEmpty()
            .WithMessage("Private message ID is required");
        RuleFor(d => d.Text)
            .NotEmpty()
            .WithMessage("Text is required")
            .MaximumLength(1000)
            .WithMessage("Text cannot exceed 1000 characters");
    }
}
