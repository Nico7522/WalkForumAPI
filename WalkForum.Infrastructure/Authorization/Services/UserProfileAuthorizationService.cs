

using WalkForum.Application.Users;
using WalkForum.Domain.AuthorizationInterfaces;
using WalkForum.Domain.Constants;
using WalkForum.Domain.Entities;

namespace WalkForum.Infrastructure.Authorization.Services;

internal class UserProfileAuthorizationService(IUserContext userContext) : IUserProfileAuthorizationService
{
    public bool Authorize(UserProfile entity, ResourceOperation resourceOperation)
    {
        var user = userContext.GetCurrentUser();

        return resourceOperation switch
        {
            ResourceOperation.Update => entity.UserId == user.Id,
            _ => false
        };
    }
}
