using WalkForum.Domain.Entities;

namespace WalkForum.Domain.Repositories;

public interface IPostsRepository
{
    Task<IEnumerable<Post>> GetAllPosts(string category);
    Task<Post?> GetPost(int id);
    Task<int> Create(Post post);
}
