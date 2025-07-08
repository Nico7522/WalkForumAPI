using AutoMapper;
using MediatR;
using WalkForum.Domain.AuthorizationInterfaces;
using WalkForum.Domain.Constants;
using WalkForum.Domain.Exceptions;
using WalkForum.Domain.Repositories;

namespace WalkForum.Application.UserProfile.Commands.UpdateUserProfileCommand;

internal class UpdateUserProfileCommandHandler(
    IUserProfileRepository userProfileRepository,
    IMapper mapper,
    IUserProfileAuthorizationService userProfileAuthorizationService) : IRequestHandler<UpdateUserProfileCommand>
{
    public async Task Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
    {
        var userProfile = await userProfileRepository.GetById(request.ProfileId);
        if (userProfile is null) throw new NotFoundException("Profile not found");

        if(!userProfileAuthorizationService.Authorize(userProfile, ResourceOperation.Update))
            throw new ForbiddenException("Not Authorized");

        mapper.Map(request, userProfile);

        await userProfileRepository.SaveChanges();
    }
}
