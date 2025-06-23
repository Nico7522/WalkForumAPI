
using Microsoft.AspNetCore.Http;
using WalkForum.Domain.Entities;

namespace WalkForum.Domain.Repositories;

public interface IUserProfileRepository : IRepositoryBase
{
    Task<UserProfile?> GetById(int id);

    Task UpdateAvatar(string filename, IFormFile file);
}
