using WalkForum.Domain.Entities;
using WalkForum.Infrastructure.Persistance;

namespace WalkForum.Infrastructure.Seeders;

internal class TagSeeder(ForumDbContext dbContext) : ITagSeeder
{
    public async Task Seed()
    {
        if (await dbContext.Database.CanConnectAsync())
        {
            if (!dbContext.Tags.Any())
            {
                var tags = getTags();
                dbContext.Tags.AddRange(tags);
                await dbContext.SaveChangesAsync();
            }
        }
    }

    private IEnumerable<Tag> getTags()
    {
        return [
            new() { Name = "Record"},
            new() { Name = "Vêtement"},
            new() { Name = "Nutrition"},
            new() { Name = "Débutant"},
            new() { Name = "Intermédiaire"},
            new() { Name = "Avancé"},
            ];
    }
}
