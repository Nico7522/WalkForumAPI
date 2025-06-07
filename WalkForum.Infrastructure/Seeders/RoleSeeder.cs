

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
            new (UserRoles.User),
            new (UserRoles.Moderator),
            new (UserRoles.Administrator),
            ];

        return roles;
    }
}
