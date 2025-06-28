using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WalkForum.Application.PrivateDiscussions.Commands.CreatePrivateDiscussion;
using WalkForum.Application.PrivateDiscussions.Commands.DeletePrivateDiscussion;
using WalkForum.Application.PrivateDiscussions.Dtos;
using WalkForum.Application.PrivateDiscussions.Queries.GetPrivateDiscussionById;

namespace WalkForum.API.Controllers
{
    [Route("api/private-discussions")]
    [ApiController]
    public class PrivateDiscussionsController(IMediator mediator) : ControllerBase
    {

        [HttpGet("{id}")]
        public async Task<ActionResult<PrivateDiscussionDto>> GetById([FromRoute] int id)
        {
            var privateDiscussion = await mediator.Send(new GetPrivateDiscussionByIdQuery(id));
            return Ok(privateDiscussion);
        }
        /// <summary>
        /// Used to create a private discussion between two profiles when it didn't exist yet.
        /// <summary>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreatePrivateDiscussionCommand command)
        {
            await mediator.Send(command);
            return Created();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await mediator.Send(new DeletePrivateDiscussionCommand(id));
            return NoContent(); 
        }
    }
}
