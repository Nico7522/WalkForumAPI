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
        try
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Avatars", filename);

            string? directory = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            await dbContext.SaveChangesAsync();
        }
        catch (DirectoryNotFoundException ex)
        {
            throw new Exception("Directory not found");
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new Exception("Permission denied");
        }
        catch (IOException ex)
        {
            throw new Exception("Input/Output error");
        }
        catch (Exception ex)
        {
            throw new Exception("Something went wrong during saving the picture");
        }
    }
}
