
using FluentValidation;

namespace WalkForum.Application.Messages.Commands.UpdateMessage;

public class UpdateMessageCommandValidator : AbstractValidator<UpdateMessageCommand>
{

    public UpdateMessageCommandValidator()
    {
        RuleFor(msg => msg.Text).NotEmpty();
    }
}
