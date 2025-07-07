using Microsoft.EntityFrameworkCore;
using WalkForum.Domain.Entities;
using WalkForum.Domain.Repositories;
using WalkForum.Infrastructure.Persistance;

namespace WalkForum.Infrastructure.Repositories;

internal class PrivateDiscussionRepository(ForumDbContext dbContext) : IPrivateDiscussionRepository
{
    public async Task Create(PrivateDiscussion privateDiscussion)
    {
        dbContext.PrivateDiscussion.Add(privateDiscussion);
        await dbContext.SaveChangesAsync();
    }

    public async Task Delete(PrivateDiscussion privateDiscussion)
    {
        dbContext.Remove(privateDiscussion);
        await dbContext.SaveChangesAsync();
    }

    public Task<PrivateDiscussion?> GetById(int id)
    {
        return dbContext.PrivateDiscussion
            .Include(d => d.PrivateMessages)
            .ThenInclude(pm => pm.UserProfile)
            .ThenInclude(p => p.User)
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task SaveChanges()
    {
        await dbContext.SaveChangesAsync();
    }
}
