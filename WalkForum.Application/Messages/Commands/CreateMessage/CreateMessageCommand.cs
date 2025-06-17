using MediatR;
using System.Text.Json.Serialization;

namespace WalkForum.Application.Messages.Commands.CreateMessage;

public class CreateMessageCommand : IRequest
{
    public string Text { get; set; } = default!;

    [JsonIgnore]
    public int PostId { get; set; }
}
