namespace WalkForum.Domain.Entities;

public class UserProfile
{
    public int Id { get; set; }
    public string? Presentation { get; set; }
    public string? Avatar { get; set; }

    public DateTime CreationDate { get; set; }
    public DateTime UpdateDate { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = default!;

    public List<PrivateDiscussion> PrivateDiscussions { get; set; } = new();
    public List<PrivateMessage> PrivateMessages { get; set; } = new();
}
