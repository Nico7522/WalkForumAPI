
using MediatR;
using Microsoft.Extensions.Logging;
using WalkForum.Application.Users;
using WalkForum.Domain.Constants;
using WalkForum.Domain.Exceptions;
using WalkForum.Domain.Repositories;

namespace WalkForum.Application.Messages.Commands.DeleteMessage;

public class DeleteMessageForPostCommandHandler(ILogger<DeleteMessageForPostCommandHandler> logger, 
    IPostsRepository postsRepository,
    IMessagesRepository messagesRepository,
    IUserContext userContext
    ) : IRequestHandler<DeleteMessageForPostCommand>
{
    public async Task Handle(DeleteMessageForPostCommand request, CancellationToken cancellationToken)
    {
       logger.LogWarning("Removing message with id {messageId}, from post with id {postId}", request.MessageId, request.PostId);


        var post = await postsRepository.GetById(request.PostId);
        if (post is null) throw new NotFoundException("Post not found");

        var message = await messagesRepository.GetById(request.MessageId);
        if (message is null) throw new NotFoundException("Message not found");

        if (message.PostId != post.Id) throw new BadRequestException("Message not in this post");

        if (message.UserId != userContext.GetCurrentUser().Id && !userContext.GetCurrentUser().IsInRole(UserRoles.Administrator) && !userContext.GetCurrentUser().IsInRole(UserRoles.Moderator)) throw new ForbiddenException("Not authorized");
        
        await messagesRepository.Delete(message);

    }
}
