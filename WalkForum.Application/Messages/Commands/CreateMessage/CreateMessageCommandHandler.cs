using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using WalkForum.Domain.Entities;
using WalkForum.Domain.Exceptions;
using WalkForum.Domain.Repositories;

namespace WalkForum.Application.Messages.Commands.CreateMessage;

internal class CreateMessageCommandHandler(ILogger<CreateMessageCommandHandler> logger, 
    IPostsRepository postsRepository, 
    IMessagesRepository messagesRepository, 
    IMapper mapper,
    IValidator<CreateMessageCommand> validator)
    : IRequestHandler<CreateMessageCommand>
{
    public async Task Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {

        logger.LogInformation("Creating new message: {@Message}", request);
        var validation = validator.Validate(request);
        if (!validation.IsValid)
        {
            throw new Domain.Exceptions.ValidationException(validation.ToDictionary());
        }
        var post = await postsRepository.GetById(request.PostId);
        if (post is null) throw new NotFoundException("Post not found");


        var message = mapper.Map<Message>(request);

        message.CreationDate = DateTime.Now;
        message.UpdateDate = DateTime.Now;
        message.UserId = 1;

        await messagesRepository.Create(message);


    }
}
