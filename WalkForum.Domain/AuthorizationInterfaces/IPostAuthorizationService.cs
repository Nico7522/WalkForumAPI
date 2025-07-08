using WalkForum.Domain.Constants;
using WalkForum.Domain.Entities;

namespace WalkForum.Domain.AuthorizationInterfaces;

public interface IPostAuthorizationService 
{
    bool Authorize(Post post, ResourceOperation resourceOperation);
}
