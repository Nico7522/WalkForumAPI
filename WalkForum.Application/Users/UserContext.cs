
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using WalkForum.Domain.Exceptions;

namespace WalkForum.Application.Users;

public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public CurrentUser GetCurrentUser()
    {
        var user = httpContextAccessor?.HttpContext?.User;
        if (user is null) throw new InvalidOperationException("User context is not present");

        if (user.Identity is null || !user.Identity.IsAuthenticated)
        {
             throw new UnauthorizedException("Unauthenticated user");  
        }

        var isIdConverted = Int32.TryParse(user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value, out int userId);
        if (!isIdConverted) throw new InvalidCastException("Something went wrong");
       

        var email = user.FindFirst(c => c.Type == ClaimTypes.Email)!.Value;
        var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

        return new CurrentUser(userId, email, roles);

    }
}
