
namespace WalkForum.Application.Messages.Dtos;

//public class MessageDto
//{
//    public int Id { get; set; }
//    public string Text { get; set; } = default!;
//    public DateTime CreationDate { get; set; }
//    public DateTime UpdateDate { get; set; }
//    public int UserId { get; set; }
//    public string Username { get; set; } = default!;

//}

public record MessageDto(string id, string text, DateTime creationDate, DateTime updateDate, int userId, string username);