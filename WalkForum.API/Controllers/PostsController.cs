using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WalkForum.Application.Posts.Commands.CreatePost;
using WalkForum.Application.Posts.Commands.DeletePost;
using WalkForum.Application.Posts.Commands.UpdatePost;
using WalkForum.Application.Posts.Dtos;
using WalkForum.Application.Posts.Queries.GetAllPosts;
using WalkForum.Application.Posts.Queries.GetPostById;

namespace WalkForum.API.Controllers;

[ApiController]
[Route("api/posts")]
public class PostsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PostDto>>> GetAll([FromQuery] string category)
    {
            var posts = await mediator.Send(new GetAllPostsQuery(category));
            return Ok(posts);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PostDto>> GetById([FromRoute]int id)
    {
        var post = await mediator.Send(new GetPostByIdQuery(id));
        return Ok(post);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] CreatePostCommand createPostCommand)
    {
        int id = await mediator.Send(createPostCommand);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //[TypeFilter(typeof(CanDeleteAuthorizationFilter))]
    [Authorize]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await mediator.Send(new DeletePostCommand(id));
        return NoContent();
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize]
    public async Task<IActionResult> Update(UpdatePostCommand updatePostCommand, [FromRoute] int id) {

        updatePostCommand.Id = id;
        await mediator.Send(updatePostCommand);

        return NoContent();
    }
}
