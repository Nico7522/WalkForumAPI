using WalkForum.Domain.Entities;

namespace WalkForum.Domain.Repositories;

public interface ITagsRepository : IRepositoryBase
{
    Task<Tag?> GetById(int id);
}
