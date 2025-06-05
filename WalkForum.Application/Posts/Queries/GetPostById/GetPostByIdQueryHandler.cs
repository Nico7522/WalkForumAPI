
using Microsoft.Extensions.Logging;
using AutoMapper;
using MediatR;
using WalkForum.Application.Posts.Dtos;
using WalkForum.Domain.Repositories;
using WalkForum.Domain.Exceptions;

namespace WalkForum.Application.Posts.Queries.GetPostById;

public class GetPostByIdQueryHandler(IPostsRepository postsRepository, IMapper mapper, ILogger<GetPostByIdQueryHandler> logger) : IRequestHandler<GetPostByIdQuery, PostDto>
{
    public async Task<PostDto> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting post with ID {postId}", request.Id);
        var post = await postsRepository.GetById(request.Id);
        if (post is null) throw new BadRequestException("Post not found");

        return mapper.Map<PostDto>(post);

    }
}
