using MediatR;
using System.Text.Json.Serialization;

namespace WalkForum.Application.PrivateMessages.Commands.CreatePrivateMessage;

public class CreatePrivateMessageCommand : IRequest
{
    [JsonIgnore]
    public int PrivateDiscussionId { get; set; }
    public string Text { get; set; } = default!;
}
