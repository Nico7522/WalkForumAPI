using WalkForum.Application.Posts.Dtos;
using WalkForum.Domain.Entities;

namespace WalkForum.Application.Posts
{
    public interface IPostsService
    {
        Task<IEnumerable<PostDto>> GetAllPosts(string category);
        Task<PostDto?> Get(int id);

    }
}