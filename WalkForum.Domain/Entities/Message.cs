namespace WalkForum.Domain.Entities;

public class Message
{
    public int Id { get; set; }
    public string Text { get; set; } = default!;

    public DateOnly CreationDate { get; set; }
    public DateOnly UpdateDate { get; set; }


    public int UserId { get; set; }
    public User User { get; set; } = default!;

    public int PostId  { get; set; }
    public Post Post { get; set; } = default!;


}
