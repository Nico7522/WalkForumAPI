using FluentValidation;

namespace WalkForum.Application.Messages.Commands.CreateMessage;

public class CreateMessageCommandValidator : AbstractValidator<CreateMessageCommand>
{
    public CreateMessageCommandValidator()
    {
        RuleFor(msg => msg.Text).NotEmpty();
    }
}
