
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using WalkForum.Domain.Entities;

namespace WalkForum.Infrastructure.Authorization;

public class WalkForumUserClaimsPrincipalFactory(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager, IOptions<IdentityOptions> options) : UserClaimsPrincipalFactory<User, IdentityRole<int>>(userManager, roleManager, options)
{
    public override async Task<ClaimsPrincipal> CreateAsync(User user)
    {
       var id = await GenerateClaimsAsync(user);

            id.AddClaim(new Claim("Id", user.Id.ToString()));

        return new ClaimsPrincipal(id);
    }
}
