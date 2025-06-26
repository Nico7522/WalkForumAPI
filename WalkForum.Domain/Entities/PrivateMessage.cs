

namespace WalkForum.Domain.Entities;

public class PrivateMessage
{
    public int Id { get; set; }
    public string text { get; set; } = default!;
    public DateTime CreationDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public int PrivateDiscussionId { get; set; }
    public PrivateDiscussion PrivateDiscussion { get; set; } = default!;
    public int UserProfileId { get; set; }
    public UserProfile UserProfile { get; set; } = default!;
}
