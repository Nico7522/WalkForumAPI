using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WalkForum.Application.Tags.Command.UpdateTagsForPost;

namespace WalkForum.API.Controllers
{

    [Route("api/posts/{postId}/tags")]
    [ApiController]
    public class TagsController(IMediator mediator) : ControllerBase
    {
        [HttpPatch]
        public async Task<IActionResult> Update([FromRoute] int postId, [FromBody] UpdateTagsForPostCommand command)
        {
            command.PostId = postId;
            await mediator.Send(command);
            return NoContent();
        }
    }
}
