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

    public async Task<IEnumerable<Post>> GetAllPosts(string category)
    {
       var posts = await dbContext.Posts
            .Include(p => p.Author)
            .Include(p => p.Category)   
            .Where(p => p.Category.Name == category)
            .ToListAsync();
       return posts;
    }

    public async Task<Post?> GetPost(int id)
    {
        var post = await dbContext.Posts
            .Include(p => p.Category)
            .Include(p => p.Author)
            .FirstOrDefaultAsync(p => p.Id == id);
        return post;
    }
}
