using Microsoft.AspNetCore.Identity;

namespace WalkForum.Domain.Entities;

public class User : IdentityUser<int>
{
    //public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Surname { get; set; } = default!;

    public ICollection<Post> Posts { get; set; } = new List<Post>();
    public ICollection<Message> Messages { get; set; } = new List<Message>();

}
