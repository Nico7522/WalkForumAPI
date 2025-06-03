using MediatR;
using Microsoft.AspNetCore.Mvc;
using WalkForum.Application.Posts.Commands.CreatePost;
using WalkForum.Application.Posts.Commands.DeletePost;
using WalkForum.Application.Posts.Commands.UpdatePost;
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
        try
        {
            var posts = await mediator.Send(new GetAllPostsQuery(category));
            return Ok(posts);

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute]int id)
    {
        var post = await mediator.Send(new GetPostByIdQuery(id));
        return post is not null ? Ok(post) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePostCommand createPostCommand)
    {
        try
        {
            int id = await mediator.Send(createPostCommand);
            return CreatedAtAction(nameof(GetById), new { id }, null);

        }
        catch (ValidationException e)
        {
            return BadRequest(new { Message = e.Message, Errors = e.Errors });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var isDeleted = await mediator.Send(new DeletePostCommand(id));
        if (isDeleted) return NoContent();

        return NotFound();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(UpdatePostCommand updatePostCommand, [FromRoute] int id) {


        try
        {
            updatePostCommand.Id = id;
            var isUpdated = await mediator.Send(updatePostCommand);

            if (isUpdated) return NoContent();

            return NotFound();


        }
        catch (ValidationException e)
        {

            return BadRequest(new { Message = e.Message, Errors = e.Errors });
        }
    }
}
