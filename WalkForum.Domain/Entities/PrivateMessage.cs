

namespace WalkForum.Domain.Entities;

public class PrivateMessage
{
    public int Id { get; set; }
    public int PrivateDiscussionId { get; set; }
    public PrivateDiscussion PrivateDiscussion { get; set; } = default!;

}
