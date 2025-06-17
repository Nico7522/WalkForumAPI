
namespace WalkForum.Application.Messages.Dtos;


public record MessageDto(int id,
    string text,
    DateTime creationDate,
    DateTime updateDate,
    int userId,
    string username);