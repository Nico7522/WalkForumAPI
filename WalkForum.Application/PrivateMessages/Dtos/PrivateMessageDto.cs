

namespace WalkForum.Application.PrivateMessages.Dtos;

public record PrivateMessageDto(int id, string text, DateTime creationDate, DateTime updateDate, int userProfileId, string username);

