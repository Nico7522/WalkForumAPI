

using MediatR;
using WalkForum.Application.Posts.Dtos;

namespace WalkForum.Application.Posts.Queries.GetPostById;

public class GetPostByIdQuery(int id) : IRequest<PostDto>
{
    public int Id { get; } = id;

}
