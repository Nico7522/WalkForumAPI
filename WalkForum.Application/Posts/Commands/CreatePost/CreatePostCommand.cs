

using MediatR;

namespace WalkForum.Application.Posts.Commands.CreatePost;

public class CreatePostCommand : IRequest<int>
{
    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;
    public int CategoryId { get; set; }
    public List<int> Tags { get; set; } = default!;

}

