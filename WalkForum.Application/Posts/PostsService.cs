
using WalkForum.Application.Posts.Dtos;
using WalkForum.Domain.Entities;
using WalkForum.Domain.Repositories;

namespace WalkForum.Application.Posts;

public class PostsService(IPostsRepository postsRepository) : IPostsService
{

    public async Task<IEnumerable<PostDto>> GetAllPosts(string category)
    {
        var posts = await postsRepository.GetAllPosts(category);
        return posts.Select(PostDto.FromEntity);
    }
    public async Task<PostDto?> Get(int id)
    {
        var post = await postsRepository.GetPost(id);
        if (post is not null) return PostDto.FromEntity(post);

        return null;
    }
}
