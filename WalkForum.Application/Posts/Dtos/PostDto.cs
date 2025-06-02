using WalkForum.Application.Categories;
using WalkForum.Application.Messages;
using WalkForum.Application.Tags;
using WalkForum.Application.Users;
using WalkForum.Domain.Entities;

namespace WalkForum.Application.Posts.Dtos;

public class PostDto
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;
    public DateOnly CreationDate { get; set; }
    public DateOnly UpdateDate { get; set; }

    public CategoryDto Category { get; set; } = default!;

    public UserDto Author { get; set; } = default!;

    public List<TagDto> Tags { get; set; } = new();
    public List<MessageDto> Messages { get; set; } = new();

    public static PostDto FromEntity(Post post)
    {
        return new PostDto()
        {
            Id = post.Id,
            Title = post.Title,
            Content = post.Content,
            CreationDate = post.CreationDate,
            UpdateDate = post.UpdateDate,
            Category = new CategoryDto() { Id = post.CategoryId, Name = post.Category.Name},
            Author = UserDto.FromEntity(post.Author),
            Tags = new List<TagDto>(),
            Messages = new List<MessageDto>(),
        };
    }
}
