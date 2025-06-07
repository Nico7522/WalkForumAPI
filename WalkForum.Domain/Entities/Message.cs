namespace WalkForum.Domain.Entities;

public class Message
{
    public int Id { get; set; }
    public string Text { get; set; } = default!;

    public DateTime CreationDate { get; set; }
    public DateTime UpdateDate { get; set; }


    public int UserId { get; set; } = default!;
    public User User { get; set; } = default!;

    public int PostId  { get; set; }
    public Post Post { get; set; } = default!;


}
