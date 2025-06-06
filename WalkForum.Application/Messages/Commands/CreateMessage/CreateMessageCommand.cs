
using MediatR;
using WalkForum.Domain.Entities;

namespace WalkForum.Application.Messages.Commands.CreateMessage;

public class CreateMessageCommand : IRequest
{
    public string Text { get; set; } = default!;
    public int PostId { get; set; }
}
