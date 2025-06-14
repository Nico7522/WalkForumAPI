using WalkForum.Domain.Entities;

namespace WalkForum.Domain.Repositories;

public interface ICategoryRepository : IRepositoryBase
{
    Task<Category?> GetByName(string name); 
}
