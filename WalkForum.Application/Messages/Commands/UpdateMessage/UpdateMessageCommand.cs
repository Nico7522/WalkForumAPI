using MediatR;


namespace WalkForum.Application.Messages.Commands.UpdateMessage;

public class UpdateMessageCommand : IRequest
{
    public int Id { get; set; }
    public string Text { get; set; } = default!;
    public int PostId { get; set; }
}
