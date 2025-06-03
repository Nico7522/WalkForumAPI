using WalkForum.Domain.Entities;

namespace WalkForum.Domain.Repositories;

public interface ICategoryRepository
{
    Task<Category?> GetByName(string name); 
}
