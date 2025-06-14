using WalkForum.Domain.Entities;

namespace WalkForum.Domain.Repositories;

public interface IPostsRepository : IRepositoryBase
{
    Task<IEnumerable<Post>> GetAll(string category);
    Task<Post?> GetById(int id);
    Task<int> Create(Post post);

    Task<bool> Delete(Post entity);

}
