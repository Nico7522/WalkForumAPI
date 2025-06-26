using MediatR;
using WalkForum.Application.Users;
using WalkForum.Domain.Exceptions;
using WalkForum.Domain.Repositories;

namespace WalkForum.Application.PrivateDiscussions.Commands.DeletePrivateDiscussion;


internal class DeletePrivateDiscussionCommandHandler(IPrivateDiscussionRepository privateDiscussionRepository,
    IUserProfileRepository userProfileRepository,
    IUserContext userContext) : IRequestHandler<DeletePrivateDiscussionCommand>
{
    public async Task Handle(DeletePrivateDiscussionCommand request, CancellationToken cancellationToken)
    {
        var privateDiscussion = await privateDiscussionRepository.GetById(request.Id);
        if (privateDiscussion is null) throw new NotFoundException("Private discussion not found");

        var userProfile = await userProfileRepository.GetByUserId(userContext.GetCurrentUser().Id);
        if (userProfile is null) throw new NotFoundException("Not authorized");

        if (!privateDiscussion.UserProfiles.Any(up => up.Id == userProfile.Id))
            throw new ForbiddenException("Not Authorized");

        await privateDiscussionRepository.Delete(privateDiscussion);
    }
}
