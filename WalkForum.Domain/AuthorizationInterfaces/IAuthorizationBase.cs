
using WalkForum.Domain.Constants;

namespace WalkForum.Domain.AuthorizationInterfaces;

public interface IAuthorizationBase<T> where T : class
{

    bool Authorize(T entity, ResourceOperation resourceOperation);
}
