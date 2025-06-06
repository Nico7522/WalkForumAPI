using WalkForum.Application.Categories.Dtos;
using WalkForum.Application.Messages;
using WalkForum.Application.Messages.Dtos;
using WalkForum.Application.Tags;
using WalkForum.Application.Users.Dtos;
using WalkForum.Domain.Entities;

namespace WalkForum.Application.Posts.Dtos;

public class PostDto
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;
    public DateTime CreationDate { get; set; }
    public DateTime UpdateDate { get; set; }

    public CategoryDto Category { get; set; } = default!;

    public UserDto Author { get; set; } = default!;

    public List<TagDto> Tags { get; set; } = new();
    public List<MessageDto> Messages { get; set; } = new();

}
