using Microsoft.AspNetCore.Mvc;
using WalkForum.Application.Posts;
using WalkForum.Domain.Exceptions;

namespace WalkForum.API.Controllers;

[ApiController]

[Route("api/posts")]
public class PostsController(IPostsService postsService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string category)
    {
        var posts = await postsService.GetAllPosts(category);
        return Ok(posts);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute]int id)
    {
        try { 
        var post = await postsService.Get(id);
        return post is not null ? Ok(post) : NotFound();
        
        }
        catch(NotFoundException e) { 
            return BadRequest(e.Message);
        }
    }
}
