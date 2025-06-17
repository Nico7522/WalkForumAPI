using MediatR;
using System.Text.Json.Serialization;


namespace WalkForum.Application.Messages.Commands.UpdateMessage;

public class UpdateMessageCommand : IRequest
{
    [JsonIgnore]
    public int Id { get; set; }
    public string Text { get; set; } = default!;

    [JsonIgnore]
    public int PostId { get; set; }
}
