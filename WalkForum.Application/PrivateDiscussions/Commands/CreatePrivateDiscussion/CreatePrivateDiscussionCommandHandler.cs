
using MediatR;
using WalkForum.Domain.Entities;
using WalkForum.Domain.Exceptions;
using WalkForum.Domain.Repositories;

namespace WalkForum.Application.PrivateDiscussions.Commands.CreatePrivateDiscussion;

internal class CreatePrivateDiscussionCommandHandler(IUserProfileRepository userProfileRepository,
    IPrivateDiscussionRepository privateDiscussionRepository) : IRequestHandler<CreatePrivateDiscussionCommand>
{
    public async Task Handle(CreatePrivateDiscussionCommand request, CancellationToken cancellationToken)
    {
        var sender = await userProfileRepository.GetById(request.SenderId);
        if (sender is null) throw new NotFoundException("Sender not found"); 

        var receiver = await userProfileRepository.GetById(request.ReceiverId);
        if(receiver is null) throw new NotFoundException("Receiver not found");

        var privateDiscussion = new PrivateDiscussion();
        privateDiscussion.PrivateMessages.Add(new PrivateMessage
        {
            text = request.Message,
            UserProfileId = request.SenderId,
            CreationDate = DateTime.Now,
            UpdateDate = DateTime.Now
        });
        privateDiscussion.UserProfiles.AddRange(sender, receiver);
        await privateDiscussionRepository.Create(privateDiscussion);
    }
}
