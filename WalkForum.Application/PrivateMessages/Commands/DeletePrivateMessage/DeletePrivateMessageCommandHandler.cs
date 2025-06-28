
using MediatR;
using WalkForum.Application.Users;
using WalkForum.Domain.Exceptions;
using WalkForum.Domain.Repositories;

namespace WalkForum.Application.PrivateMessages.Commands.DeletePrivateMessage;

internal class DeletePrivateMessageCommandHandler(
    IPrivateDiscussionRepository privateDiscussionRepository,
    IPrivateMessageRepository privateMessageRepository,
    IUserProfileRepository userProfileRepository,
    IUserContext userContext
    ) : IRequestHandler<DeletePrivateMessageCommand>
{
    public async Task Handle(DeletePrivateMessageCommand request, CancellationToken cancellationToken)
    {
        // Check if private discussion exist
        var privateDiscussion = await privateDiscussionRepository.GetById(request.PrivateDiscussionId);
        if (privateDiscussion is null) throw new NotFoundException("Private discussion not found");

        // Check if user profile exist
        var userProfile = await userProfileRepository.GetByUserId(userContext.GetCurrentUser().Id);
        if (userProfile is null) throw new NotFoundException("Profile not found");

        // Check if private message exist
        var privateMessage = await privateMessageRepository.GetById(request.PrivateMessageId);
        if (privateMessage is null) throw new NotFoundException("Private message not found");

        // Check if user is authorized to update the message
        if (privateMessage.UserProfileId != userProfile.Id)
            throw new ForbiddenException("Not authorized to delete this message");

        // Check if user is part of the private discussion
        if (!privateDiscussion.UserProfiles.Any(p => p.Id == userProfile.Id))
            throw new ForbiddenException("Not authorized");

        // Check if private message is part of the discussion
        if (!privateDiscussion.PrivateMessages.Any(p => p.Id == request.PrivateMessageId))
            throw new NotFoundException("Private message not found in discussion");

        await privateMessageRepository.Delete(privateMessage);
    }
}
