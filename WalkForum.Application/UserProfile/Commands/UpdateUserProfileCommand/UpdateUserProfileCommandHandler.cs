using AutoMapper;
using MediatR;
using WalkForum.Application.Users;
using WalkForum.Domain.Constants;
using WalkForum.Domain.Exceptions;
using WalkForum.Domain.Repositories;

namespace WalkForum.Application.UserProfile.Commands.UpdateUserProfileCommand;

internal class UpdateUserProfileCommandHandler(IUserProfileRepository userProfileRepository,
    IMapper mapper,
    IUserContext userContext) : IRequestHandler<UpdateUserProfileCommand>
{
    public async Task Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
    {
        
        var userProfile = await userProfileRepository.GetById(request.ProfileId);
        if (userProfile is null) throw new NotFoundException("Profile not found");

        if (userContext.GetCurrentUser().Id != userProfile.UserId && !userContext.GetCurrentUser().IsInRole(UserRoles.Administrator)) throw new ForbiddenException("Not authorized");

        mapper.Map(request, userProfile);

        await userProfileRepository.SaveChanges();
    }
}
