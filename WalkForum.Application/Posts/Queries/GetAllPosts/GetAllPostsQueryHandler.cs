

using AutoMapper;
using MediatR;
using WalkForum.Application.Posts.Dtos;
using WalkForum.Domain.Entities;
using WalkForum.Domain.Repositories;

namespace WalkForum.Application.Posts.Queries.GetAllPosts;

internal class GetAllPostsQueryHandler(IPostsRepository postsRepository, IMapper mapper) : IRequestHandler<GetAllPostsQuery, IEnumerable<PostDto>>
{
    public async Task<IEnumerable<PostDto>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
    {
        var posts = await postsRepository.GetAllPosts(request.Category);
        var postsDto = mapper.Map<IEnumerable<PostDto>>(posts);
        return postsDto;
    }
}
