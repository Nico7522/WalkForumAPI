

using MediatR;

namespace WalkForum.Application.Posts.Commands.CreatePost;

public class CreatePostCommand : IRequest<int>
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;
    public DateTime CreationDate { get; set; } = DateTime.Now;
    public DateTime UpdateDate { get; set; } = DateTime.Now;
    public int CategoryId { get; set; }
    public int AuthorId { get; set; }
}
