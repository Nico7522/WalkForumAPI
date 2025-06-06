
using WalkForum.Domain.Entities;

namespace WalkForum.Application.Messages.Dtos;

public class MessageDto
{
    public int Id { get; set; }
    public string Text { get; set; } = default!;
    public DateTime CreationDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public int UserId { get; set; }
    public string Username { get; set; } = default!;

}
