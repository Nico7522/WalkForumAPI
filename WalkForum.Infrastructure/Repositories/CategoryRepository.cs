


using Microsoft.EntityFrameworkCore;
using WalkForum.Domain.Entities;
using WalkForum.Domain.Repositories;
using WalkForum.Infrastructure.Persistance;

namespace WalkForum.Infrastructure.Repositories;

internal class CategoryRepository(ForumDbContext dbContext) : ICategoryRepository
{
    public async Task<Category?> GetById(int id)
    {
        return await dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id); 
    }

    public async Task<Category?> GetByName(string name)
    {
      return await dbContext.Categories.FirstOrDefaultAsync(c => c.Name.ToLower() == name.ToLower());  
    }

    public Task SaveChanges()
    {
        throw new NotImplementedException();
    }
}
