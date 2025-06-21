
using MediatR;

namespace WalkForum.Application.Tags.Command.UpdateTagsForPost;

public class UpdateTagsForPostCommand : IRequest
{
    public List<int> Tags { get; set; } = default!;
    public int PostId { get; set; }
}
