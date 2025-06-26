using MediatR;

namespace WalkForum.Application.PrivateDiscussions.Commands.CreatePrivateDiscussion;

public class CreatePrivateDiscussionCommand : IRequest
{
    public int SenderId { get; set; }
    public int ReceiverId { get; set; }
    public string Message { get; set; } = default!;
}
