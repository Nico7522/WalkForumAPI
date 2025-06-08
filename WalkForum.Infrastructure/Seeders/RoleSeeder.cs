

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WalkForum.Domain.Constants;
using WalkForum.Infrastructure.Persistance;

namespace WalkForum.Infrastructure.Seeders;

internal class RoleSeeder(ForumDbContext dbContext) : IRoleSeeder
{
    public async Task Seed()
    {
        if (await dbContext.Database.CanConnectAsync())
        {
            if (!dbContext.Roles.Any())
            {
                var roles = getRoles();
                dbContext.Roles.AddRange(roles);
                await dbContext.SaveChangesAsync();
            }
        }
    }
    private IEnumerable<IdentityRole<int>> getRoles()
    {
        List<IdentityRole<int>> roles = [
            new (UserRoles.User) {
                NormalizedName = UserRoles.User.ToUpper()
            },
            new (UserRoles.Moderator) {
                NormalizedName = UserRoles.Moderator.ToUpper()
            },
            new (UserRoles.Administrator) { NormalizedName = UserRoles.Administrator.ToUpper() },
            ];

        return roles;
    }
}
