

using MediatR;
using WalkForum.Application.Posts.Dtos;

namespace WalkForum.Application.Posts.Queries.GetAllPosts;

public class GetAllPostsQuery(string category) : IRequest<IEnumerable<PostDto>>
{
    public string Category { get; } = category;


}
