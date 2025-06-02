
using WalkForum.Domain.Entities;
using WalkForum.Infrastructure.Persistance;

namespace WalkForum.Infrastructure.Seeders;

internal class CategorySeeder(ForumDbContext dbContext) : ICategorySeeder
{
    public async Task Seed()
    {
        if (await dbContext.Database.CanConnectAsync())
        {
            if (!dbContext.Categories.Any())
            {
                var categories = getCategories();
                dbContext.Categories.AddRange(categories);
                await dbContext.SaveChangesAsync();
            }
        }
    }

    private IEnumerable<Category> getCategories()
    {
        return [
            new() {
                Name = "Discussion"
            },
            new() {
                Name = "Aide"
            },
            new() {
                Name = "Carnets de marche"
            }
        ];
    }
}
