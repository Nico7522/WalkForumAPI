using MediatR;


namespace WalkForum.Application.PrivateDiscussions.Commands.DeletePrivateDiscussion;

public class DeletePrivateDiscussionCommand(int id) : IRequest
{
    public int Id { get; } = id;
}
