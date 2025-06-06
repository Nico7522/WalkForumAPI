
using MediatR;

namespace WalkForum.Application.Messages.Commands.DeleteMessage;

public class DeleteMessageForPostCommand(int postId, int messageId) : IRequest
{
    public int PostId { get; } = postId; 
    public int MessageId { get; } = messageId;
}
