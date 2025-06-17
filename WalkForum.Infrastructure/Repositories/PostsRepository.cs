using Microsoft.EntityFrameworkCore;
using WalkForum.Domain.Entities;
using WalkForum.Domain.Repositories;
using WalkForum.Infrastructure.Persistance;

namespace WalkForum.Infrastructure.Repositories;

internal class PostsRepository(ForumDbContext dbContext) : IPostsRepository
{
    public async Task<int> Create(Post entity)
    {
        dbContext.Posts.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<bool> Delete(Post entity)
    {
        dbContext.Remove(entity);
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Post>> GetAll(string category)
    {
        var posts = await dbContext.Posts
             .Include(p => p.Author)
             .Include(p => p.Category)
             .Where(p => p.Category.Name == category)
             .Include(p => p.Tags)
            .ToListAsync();
       return posts;
    }

    public async Task<Post?> GetById(int id)
    {
        var post = await dbContext.Posts
            .Include(p => p.Category)
            .Include(p => p.Author)
            .Include(p => p.Messages)
            .ThenInclude(m => m.User)
            .Include(p => p.Tags)
            .FirstOrDefaultAsync(p => p.Id == id);
        return post;
    }

    public async Task SaveChanges()
     => await dbContext.SaveChangesAsync();
}
