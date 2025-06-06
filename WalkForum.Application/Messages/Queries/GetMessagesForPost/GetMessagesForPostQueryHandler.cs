

using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using WalkForum.Application.Messages.Dtos;
using WalkForum.Domain.Exceptions;
using WalkForum.Domain.Repositories;

namespace WalkForum.Application.Messages.Queries.GetMessagesForPost;

public class GetMessagesForPostQueryHandler(ILogger<GetMessagesForPostQueryHandler> logger,
    IPostsRepository postsRepository,
    IMapper mapper) : IRequestHandler<GetMessagesForPostQuery, IEnumerable<MessageDto>>
{
    public async Task<IEnumerable<MessageDto>> Handle(GetMessagesForPostQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Retrieving messages for post with id: {postId}", request.PostId);

        var post = await postsRepository.GetById(request.PostId);
        if (post is null) throw new NotFoundException("Post not found");


        var messages = mapper.Map<IEnumerable<MessageDto>>(post.Messages);
        return messages;
    }
}
