using AutoMapper;
using FluentValidation;
using MediatR;
using WalkForum.Application.Users;
using WalkForum.Application.Utilities;
using WalkForum.Domain.Constants;
using WalkForum.Domain.Exceptions;
using WalkForum.Domain.Repositories;

namespace WalkForum.Application.Messages.Commands.UpdateMessage;

public class UpdateMessageCommandHandler(IValidator<UpdateMessageCommand> validator, 
    IPostsRepository postsRepository,
    IMessagesRepository messagesRepository,
    IUserContext userContext,
    IMapper mapper) : IRequestHandler<UpdateMessageCommand>
{
    public async Task Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
    {
        Helpers.ValidForm(request, validator);

        var post = await postsRepository.GetById(request.PostId);
        if (post is null) throw new NotFoundException("Post not found");

        var message = await messagesRepository.GetById(request.Id);
        if (message is null) throw new NotFoundException("Message not found");

        if (message.PostId != post.Id) throw new BadRequestException("Message not in this post");

        if (message.UserId != userContext.GetCurrentUser().Id && !userContext.GetCurrentUser().IsInRole(UserRoles.Administrator) && !userContext.GetCurrentUser().IsInRole(UserRoles.Moderator)) throw new ForbiddenException("Not authorized");

        mapper.Map(request, message);
        await messagesRepository.SaveChanges();

    }
}
