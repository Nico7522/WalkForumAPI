namespace WalkForum.Domain.Entities;

public class PrivateDiscussion
{
    public int Id { get; set; }
    public ICollection<PrivateMessage> PrivateMessages { get; } = new List<PrivateMessage>();
    public List<UserProfile> UserProfiles { get; } = [];
}
