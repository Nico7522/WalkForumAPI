namespace WalkForum.Domain.Entities;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;
    public DateTime CreationDate { get; set; }
    public DateTime UpdateDate { get; set; }
    
    public int CategoryId { get; set; }
    public Category Category { get; set; } = default!;

    public int AuthorId { get; set; }
    public User Author { get; set; } = default!;

    public List<Tag> Tags { get; set; } = new();
    public List<Message> Messages { get; set; } = new();

}
