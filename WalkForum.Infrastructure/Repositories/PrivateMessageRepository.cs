using Microsoft.EntityFrameworkCore;
using WalkForum.Domain.Entities;
using WalkForum.Domain.Repositories;
using WalkForum.Infrastructure.Persistance;

namespace WalkForum.Infrastructure.Repositories;

internal class PrivateMessageRepository(ForumDbContext dbContext) : IPrivateMessageRepository
{
    public async Task Delete(PrivateMessage privateMessage)
    {
        dbContext.Remove(privateMessage);
        await dbContext.SaveChangesAsync();
    }

    public async Task<PrivateMessage?> GetById(int id)
    {
        return await dbContext.PrivateMessage.FirstOrDefaultAsync(pm => pm.Id == id);
    }

    public async Task SaveChanges()
    {
        await dbContext.SaveChangesAsync();
    }
}
