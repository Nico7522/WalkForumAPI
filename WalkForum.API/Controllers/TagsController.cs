using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WalkForum.Application.Tags.Command.UpdateTagsForPost;
using WalkForum.Domain.Constants;

namespace WalkForum.API.Controllers
{

    [Route("api/posts/{postId}/tags")]
    [ApiController]
    public class TagsController(IMediator mediator) : ControllerBase
    {
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize]

        public async Task<IActionResult> Update([FromRoute] int postId, [FromBody] UpdateTagsForPostCommand command)
        {
            command.PostId = postId;
            await mediator.Send(command);
            return NoContent();
        }
    }
}
