

using Microsoft.AspNetCore.Mvc.Filters;

namespace WalkForum.Infrastructure.Filters;

public class CanDeleteAuthorizationFilter : IAuthorizationFilter
{
    void IAuthorizationFilter.OnAuthorization(AuthorizationFilterContext context)
    {
        context.RouteData.Values.TryGetValue("id", out var idValue);
        Console.WriteLine(idValue);
       
    }
}
