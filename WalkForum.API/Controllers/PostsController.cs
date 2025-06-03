using MediatR;
using Microsoft.AspNetCore.Mvc;
using WalkForum.Application.Posts.Commands.CreatePost;
using WalkForum.Application.Posts.Queries.GetAllPosts;
using WalkForum.Application.Posts.Queries.GetPostById;
using WalkForum.Domain.Exceptions;
namespace WalkForum.API.Controllers;

[ApiController]

[Route("api/posts")]
public class PostsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string category)
    {
        var posts = await mediator.Send(new GetAllPostsQuery(category));
        return Ok(posts);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute]int id)
    {
        var post = await mediator.Send(new GetPostByIdQuery(id));
        return post is not null ? Ok(post) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] CreatePostCommand createPostCommand)
    {
        try
        {
            int id = await mediator.Send(createPostCommand);
            return CreatedAtAction(nameof(Get), new { id }, null);

        }
        catch (ValidationException e)
        {
            return BadRequest(new { Message = e.Message, Errors = e.Errors });
        }
    }
}
