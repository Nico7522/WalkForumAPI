using WalkForum.Application.Users;
using WalkForum.Domain.AuthorizationInterfaces;
using WalkForum.Domain.Constants;
using WalkForum.Domain.Entities;

namespace WalkForum.Infrastructure.Authorization.Services;

internal class PostAuthorizationService(IUserContext userContext) : IPostAuthorizationService
{
    public bool Authorize(Post entity, ResourceOperation resourceOperation)
    {
        var user = userContext.GetCurrentUser();

        return resourceOperation switch
        {
            ResourceOperation.Delete => user.Id == entity.AuthorId || user.IsInRole(UserRoles.Administrator) || user.IsInRole(UserRoles.Moderator),
            ResourceOperation.Update => user.Id == entity.AuthorId ? true : false,
            _ => false
        };
    }
}
