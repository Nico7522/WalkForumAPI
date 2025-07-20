

using WalkForum.Domain.AuthorizationInterfaces;
using WalkForum.Domain.Constants;
using WalkForum.Domain.Entities;

namespace WalkForum.Infrastructure.Authorization.Services;

internal class PrivateDiscussionAuthorizationService : IPrivateDiscussionAuthorizationService
{
    public bool Authorize(PrivateDiscussion discussion, UserProfile userProfile, ResourceOperation resourceOperation)
    {
        throw new NotImplementedException();
    }

    public bool Authorize(PrivateDiscussion entity, ResourceOperation resourceOperation)
    {
        throw new NotImplementedException();
    }
}
