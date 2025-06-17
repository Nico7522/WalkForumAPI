using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using WalkForum.Application.Users;
using WalkForum.Application.Utilities;
using WalkForum.Domain.Entities;
using WalkForum.Domain.Exceptions;
using WalkForum.Domain.Repositories;

namespace WalkForum.Application.Messages.Commands.CreateMessage;

internal class CreateMessageCommandHandler(ILogger<CreateMessageCommandHandler> logger, 
    IPostsRepository postsRepository, 
    IMessagesRepository messagesRepository, 
    IMapper mapper,
    IUserContext userContext,
    IValidator<CreateMessageCommand> validator)
    : IRequestHandler<CreateMessageCommand>
{
    public async Task Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new message: {@Message}", request);
        Helpers.ValidForm(request, validator);
    

        var post = await postsRepository.GetById(request.PostId);
        if (post is null) throw new NotFoundException("Post not found");


        var message = mapper.Map<Message>(request);

        message.UserId = userContext.GetCurrentUser().Id;

        await messagesRepository.Create(message);


    }
}
