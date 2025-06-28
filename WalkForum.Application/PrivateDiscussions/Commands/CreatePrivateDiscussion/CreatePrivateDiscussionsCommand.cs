using MediatR;

namespace WalkForum.Application.PrivateDiscussions.Commands.CreatePrivateDiscussion;

public class CreatePrivateDiscussionCommand : IRequest
{
    public int ReceiverId { get; set; }
    public string Text { get; set; } = default!;
}
