using WalkForum.Domain.Constants;
using WalkForum.Domain.Entities;

namespace WalkForum.Domain.AuthorizationInterfaces;

public interface IPrivateDiscussionAuthorizationService : IAuthorizationBase<PrivateDiscussion>
{
    bool Authorize(PrivateDiscussion discussion, UserProfile userProfile, ResourceOperation resourceOperation);
}
