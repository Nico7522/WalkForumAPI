using Microsoft.Extensions.Hosting;
using WalkForum.Application.Users;
using WalkForum.Domain.AuthorizationInterfaces;
using WalkForum.Domain.Constants;
using WalkForum.Domain.Entities;

namespace WalkForum.Infrastructure.Authorization.Services;

internal class MessageAuthorizationService(IUserContext userContext) : IMessageAuthorizationService
{
    public bool Authorize(Message entity, ResourceOperation resourceOperation)
    {
        var user = userContext.GetCurrentUser();

        return resourceOperation switch
        {
            ResourceOperation.Delete => user.Id == entity.UserId || user.IsInRole(UserRoles.Administrator) || user.IsInRole(UserRoles.Moderator),
            ResourceOperation.Update => user.Id == entity.UserId ? true : false,
            _ => false
        };
    }
}
