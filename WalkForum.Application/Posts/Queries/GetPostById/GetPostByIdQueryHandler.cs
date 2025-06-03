

using AutoMapper;
using MediatR;
using WalkForum.Application.Posts.Dtos;
using WalkForum.Domain.Repositories;

namespace WalkForum.Application.Posts.Queries.GetPostById;

public class GetPostByIdQueryHandler(IPostsRepository postsRepository, IMapper mapper) : IRequestHandler<GetPostByIdQuery, PostDto?>
{
    public async Task<PostDto?> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
    {
        var post = await postsRepository.GetById(request.Id);
        if (post is not null) return mapper.Map<PostDto?>(post);


        return null;
    }
}
