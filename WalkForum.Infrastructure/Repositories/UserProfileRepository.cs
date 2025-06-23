using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WalkForum.Domain.Entities;
using WalkForum.Domain.Repositories;
using WalkForum.Infrastructure.Persistance;

namespace WalkForum.Infrastructure.Repositories;


internal class UserProfileRepository(ForumDbContext dbContext) : IUserProfileRepository
{
    public async Task<UserProfile?> GetById(int id)
    {
        return await dbContext.UserProfile.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task SaveChanges()
    {
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAvatar(string filename, IFormFile file)
    {
        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Avatars", filename);
        FileStream stream = new FileStream(path, FileMode.Create);
        file.CopyTo(stream);
        stream.Dispose();
        stream.Close();
        await dbContext.SaveChangesAsync();
    }
}
