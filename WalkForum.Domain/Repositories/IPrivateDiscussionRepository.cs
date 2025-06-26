
using WalkForum.Domain.Entities;

namespace WalkForum.Domain.Repositories;

public interface IPrivateDiscussionRepository : IRepositoryBase
{
    Task<PrivateDiscussion?> GetById(int id);
    Task Create(PrivateDiscussion privateDiscussion);
    Task Delete(PrivateDiscussion privateDiscussion);
}
