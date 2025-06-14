

using Microsoft.EntityFrameworkCore;
using WalkForum.Domain.Entities;
using WalkForum.Domain.Repositories;
using WalkForum.Infrastructure.Persistance;

namespace WalkForum.Infrastructure.Repositories;

internal class CategoryRepository(ForumDbContext dbContext) : ICategoryRepository
{
    public async Task<Category?> GetByName(string name)
    {
      return await dbContext.Categories.FirstOrDefaultAsync(c => c.Name == name);  
    }

    public Task SaveChanges()
    {
        throw new NotImplementedException();
    }
}
