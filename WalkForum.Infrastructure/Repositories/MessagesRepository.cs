using Microsoft.EntityFrameworkCore;
using WalkForum.Domain.Entities;
using WalkForum.Domain.Repositories;
using WalkForum.Infrastructure.Persistance;

namespace WalkForum.Infrastructure.Repositories;

internal class MessagesRepository(ForumDbContext dbContext) : IMessagesRepository
{
    public async Task<int> Create(Message entity)
    {
        dbContext.Messages.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }

    public async Task Delete(Message entity)
    {
        dbContext.Remove(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task<Message?> GetById(int id)
    {
        return await dbContext.Messages.FirstOrDefaultAsync(msg => msg.Id == id);
    }
}
