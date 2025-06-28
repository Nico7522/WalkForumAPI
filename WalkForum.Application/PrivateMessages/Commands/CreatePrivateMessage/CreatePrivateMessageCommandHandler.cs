using FluentValidation;
using MediatR;
using WalkForum.Application.Users;
using WalkForum.Application.Utilities;
using WalkForum.Domain.Entities;
using WalkForum.Domain.Exceptions;
using WalkForum.Domain.Repositories;

namespace WalkForum.Application.PrivateMessages.Commands.CreatePrivateMessage;

internal class CreatePrivateMessageCommandHandler(
    IPrivateDiscussionRepository privateDiscussionRepository,
    IUserProfileRepository userProfileRepository,
    IUserContext userContext,
    IValidator<CreatePrivateMessageCommand> validator) : IRequestHandler<CreatePrivateMessageCommand>
{
    public async Task Handle(CreatePrivateMessageCommand request, CancellationToken cancellationToken)
    {
        Helpers.ValidForm(request, validator);

        var privateDiscussion = await privateDiscussionRepository.GetById(request.PrivateDiscussionId);
        if (privateDiscussion is null) throw new NotFoundException("Private discussion not found");

        var userProfile = await userProfileRepository.GetByUserId(userContext.GetCurrentUser().Id);
        if(userProfile is null) throw new NotFoundException("Profile not found");

        if(!privateDiscussion.UserProfiles.Any(p => p.Id == userProfile.Id))
            throw new ForbiddenException("Not authorized");


        privateDiscussion.PrivateMessages.Add(new PrivateMessage
        {
            UserProfileId = userProfile.Id,
            text = request.Text,
            CreationDate = DateTime.Now,
            UpdateDate = DateTime.Now,
        });

        await privateDiscussionRepository.SaveChanges();
    }
}
