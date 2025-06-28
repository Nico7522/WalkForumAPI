
using WalkForum.Domain.Entities;

namespace WalkForum.Domain.Repositories;

public interface IPrivateMessageRepository : IRepositoryBase
{
    Task<PrivateMessage?> GetById(int id);
    Task Delete(PrivateMessage privateMessage);
}
