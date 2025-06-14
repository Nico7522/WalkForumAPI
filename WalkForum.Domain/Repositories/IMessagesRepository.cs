

using WalkForum.Domain.Entities;

namespace WalkForum.Domain.Repositories;

public interface IMessagesRepository : IRepositoryBase
{
    Task<int> Create(Message entity);
    Task<Message?> GetById(int id);

    Task Delete(Message entity);
}
