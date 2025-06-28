using WalkForum.Application.PrivateMessages.Dtos;

namespace WalkForum.Application.PrivateDiscussions.Dtos;

public record PrivateDiscussionDto(int id, List<PrivateMessageDto> privateMessages);

