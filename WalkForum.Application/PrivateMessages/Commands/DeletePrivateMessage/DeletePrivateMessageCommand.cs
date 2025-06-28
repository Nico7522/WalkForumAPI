

using MediatR;

namespace WalkForum.Application.PrivateMessages.Commands.DeletePrivateMessage;

public class DeletePrivateMessageCommand : IRequest
{
    public int PrivateDiscussionId { get; set; }
    public int PrivateMessageId { get; set; }
}
