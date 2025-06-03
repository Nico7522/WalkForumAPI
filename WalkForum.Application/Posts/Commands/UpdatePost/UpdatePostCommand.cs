
using MediatR;

namespace WalkForum.Application.Posts.Commands.UpdatePost;

public class UpdatePostCommand : IRequest<bool>
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public DateTime UpdateDate { get; set; } = DateTime.Now;
    public string Content { get; set; } = default!;
}
