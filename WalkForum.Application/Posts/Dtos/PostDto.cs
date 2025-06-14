using WalkForum.Application.Categories.Dtos;
using WalkForum.Application.Messages;
using WalkForum.Application.Messages.Dtos;
using WalkForum.Application.Tags;
using WalkForum.Application.Users.Dtos;
using WalkForum.Domain.Entities;

namespace WalkForum.Application.Posts.Dtos;

public record PostDto(int Id,
    string title,
    string content,
    DateTime creationDate,
    DateTime updateDate,
    CategoryDto category,
    UserDto author,
    List<TagDto> tags,
    List<MessageDto> messages);
