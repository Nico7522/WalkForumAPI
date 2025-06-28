

using AutoMapper;
using FluentValidation;
using MediatR;
using WalkForum.Application.Users;
using WalkForum.Application.Utilities;
using WalkForum.Domain.Exceptions;
using WalkForum.Domain.Repositories;

namespace WalkForum.Application.PrivateMessages.Commands.UpdatePrivateMessage;

internal class UpdatePrivateMessageCommandHandler(
    IPrivateDiscussionRepository privateDiscussionRepository,
    IPrivateMessageRepository privateMessageRepository,
    IUserProfileRepository userProfileRepository,
    IUserContext userContext,
    IValidator<UpdatePrivateMessageCommand> validator,
    IMapper mapper) : IRequestHandler<UpdatePrivateMessageCommand>
{
    public async Task Handle(UpdatePrivateMessageCommand request, CancellationToken cancellationToken)
    {
        Helpers.ValidForm(request, validator);
        // Check if private discussion exist
        var privateDiscussion = await privateDiscussionRepository.GetById(request.PrivateDiscussionId);
        if (privateDiscussion is null) throw new NotFoundException("Private discussion not found");

        // Check if user profile exist
        var userProfile = await userProfileRepository.GetByUserId(userContext.GetCurrentUser().Id);
        if (userProfile is null) throw new NotFoundException("Profile not found");

        // Check if private message exist
        var privateMessage = await privateMessageRepository.GetById(request.PrivateMessageId);
        if(privateMessage is null) throw new NotFoundException("Private message not found");

        // Check if user is authorized to update the message
        if (privateMessage.UserProfileId != userProfile.Id)
            throw new ForbiddenException("Not authorized to update this message");

        // Check if user is part of the private discussion
        if (!privateDiscussion.UserProfiles.Any(p => p.Id == userProfile.Id))
            throw new ForbiddenException("Not authorized");

        // Check if private message is part of the discussion
        if (!privateDiscussion.PrivateMessages.Any(p => p.Id == request.PrivateMessageId))
            throw new NotFoundException("Private message not found in discussion");

        mapper.Map(request, privateMessage);

        await privateMessageRepository.SaveChanges();

    }
}
