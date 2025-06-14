using AutoMapper;
using FluentValidation;
using MediatR;
using WalkForum.Application.Utilities;
using WalkForum.Domain.Exceptions;
using WalkForum.Domain.Repositories;

namespace WalkForum.Application.Messages.Commands.UpdateMessage;

public class UpdateMessageCommandHandler(IValidator<UpdateMessageCommand> validator, 
    IPostsRepository postsRepository,
    IMessagesRepository messagesRepository,
    IMapper mapper) : IRequestHandler<UpdateMessageCommand>
{
    public async Task Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
    {
        Helpers.ValidForm(request, validator);

        var post = await postsRepository.GetById(request.PostId);
        if (post is null) throw new NotFoundException("Post not found");

        var message = await messagesRepository.GetById(request.Id);
        if (message is null) throw new NotFoundException("Message not found");

        mapper.Map(request, message);

        await messagesRepository.SaveChanges();

    }
}
