
using MediatR;
using System.Text.Json.Serialization;

namespace WalkForum.Application.Posts.Commands.UpdatePost;

public class UpdatePostCommand: IRequest
{
    [JsonIgnore]
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public DateTime UpdateDate { get; set; } = DateTime.Now;
    public string Content { get; set; } = default!;
}
