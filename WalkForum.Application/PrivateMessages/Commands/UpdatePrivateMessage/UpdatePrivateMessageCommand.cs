using MediatR;

namespace WalkForum.Application.PrivateMessages.Commands.UpdatePrivateMessage;

public class UpdatePrivateMessageCommand : IRequest
{
    public int PrivateDiscussionId { get; set; }
    public int PrivateMessageId { get; set; }
    public string Text { get; set; } = default!;
}
