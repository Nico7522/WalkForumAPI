

using MediatR;

namespace WalkForum.Application.Posts.Commands.DeletePost;

public class DeletePostCommand(int id) : IRequest
{
    public int Id { get; } = id;
}
