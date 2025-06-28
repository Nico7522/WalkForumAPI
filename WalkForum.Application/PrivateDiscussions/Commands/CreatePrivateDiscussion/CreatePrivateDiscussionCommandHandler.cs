
using FluentValidation;
using MediatR;
using WalkForum.Application.Users;
using WalkForum.Application.Utilities;
using WalkForum.Domain.Entities;
using WalkForum.Domain.Exceptions;
using WalkForum.Domain.Repositories;

namespace WalkForum.Application.PrivateDiscussions.Commands.CreatePrivateDiscussion;

internal class CreatePrivateDiscussionCommandHandler(
    IUserProfileRepository userProfileRepository,
    IPrivateDiscussionRepository privateDiscussionRepository,
    IUserContext userContext,
    IValidator<CreatePrivateDiscussionCommand> validator) : IRequestHandler<CreatePrivateDiscussionCommand>
{
    public async Task Handle(CreatePrivateDiscussionCommand request, CancellationToken cancellationToken)
    {
        Helpers.ValidForm(request, validator);

        var senderProfile = await userProfileRepository.GetByUserId(userContext.GetCurrentUser().Id);
        if (senderProfile is null) throw new NotFoundException("Sender not found"); 

        var receiverProfile = await userProfileRepository.GetById(request.ReceiverId);
        if(receiverProfile is null) throw new NotFoundException("Receiver not found");

        var privateDiscussion = new PrivateDiscussion();
        privateDiscussion.PrivateMessages.Add(new PrivateMessage
        {
            text = request.Text,
            UserProfileId = senderProfile.Id,
            CreationDate = DateTime.Now,
            UpdateDate = DateTime.Now
        });
        privateDiscussion.UserProfiles.AddRange(senderProfile, receiverProfile);
        await privateDiscussionRepository.Create(privateDiscussion);
    }
}
