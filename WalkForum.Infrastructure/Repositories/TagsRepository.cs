

using Microsoft.EntityFrameworkCore;
using WalkForum.Domain.Entities;
using WalkForum.Domain.Repositories;
using WalkForum.Infrastructure.Persistance;

namespace WalkForum.Infrastructure.Repositories;

internal class TagsRepository(ForumDbContext dbContext) : ITagsRepository
{
    public async Task<Tag?> GetById(int id)
    {
        return await dbContext.Tags.FirstOrDefaultAsync(t => t.Id == id);
    }

    public Task SaveChanges()
    {
        throw new NotImplementedException();
    }
}
