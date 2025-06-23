using MediatR;
using WalkForum.Application.Users;
using WalkForum.Domain.Constants;
using WalkForum.Domain.Exceptions;
using WalkForum.Domain.Repositories;

namespace WalkForum.Application.UserProfile.Commands.UpdateAvatarUserProfile;

internal class UpdateAvatarUserProfileCommandHandler(IUserProfileRepository userProfileRepository,
    IUserContext userContext) : IRequestHandler<UpdateAvatarUserProfileCommand>
{
    public async Task Handle(UpdateAvatarUserProfileCommand request, CancellationToken cancellationToken)
    {
        var userProfile = await userProfileRepository.GetById(request.ProfileId);
        if (userProfile is null) throw new NotFoundException("Profile not found");

        if (userContext.GetCurrentUser().Id != userProfile.UserId && !userContext.GetCurrentUser().IsInRole(UserRoles.Administrator)) throw new ForbiddenException("Not authorized");

        List<string> validExtentions = new List<string>() {".jpg", ".png", ".jpeg" };
        string extention = Path.GetExtension(request.File.FileName);

        if (!validExtentions.Contains(extention)) throw new BadRequestException("Invalid file extention");

        long size = request.File.Length;
        if (size > (5 * 1024 * 1024)) throw new BadRequestException("Invalid file size");

        string filename = Guid.NewGuid().ToString() + extention;

        userProfile.Avatar = filename;
        await userProfileRepository.UpdateAvatar(filename, request.File);

    }
}
