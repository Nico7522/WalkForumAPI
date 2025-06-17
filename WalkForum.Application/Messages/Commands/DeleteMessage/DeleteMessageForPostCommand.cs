
using MediatR;
using System.Text.Json.Serialization;

namespace WalkForum.Application.Messages.Commands.DeleteMessage;

public class DeleteMessageForPostCommand(int postId, int messageId) : IRequest
{

    [JsonIgnore]
    public int PostId { get; } = postId;

    [JsonIgnore]
    public int MessageId { get; } = messageId;
}
