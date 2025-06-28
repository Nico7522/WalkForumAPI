using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WalkForum.Application.PrivateMessages.Commands.CreatePrivateMessage;
using WalkForum.Application.PrivateMessages.Commands.UpdatePrivateMessage;

namespace WalkForum.API.Controllers
{
    [Route("api/private-discussions/{privateDiscussionId}/private-messages")]
    [ApiController]
    public class PrivateMessagesController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromRoute] int privateDiscussionId, [FromBody] CreatePrivateMessageCommand command)
        {
            command.PrivateDiscussionId = privateDiscussionId;
            await mediator.Send(command);
            return Created();
        }

        [HttpPatch]
        public async Task<IActionResult> Update([FromRoute] int privateDiscussionId, [FromBody] UpdatePrivateMessageCommand command)
        {
            command.PrivateDiscussionId = privateDiscussionId;
            await mediator.Send(command);
            return NoContent();
        }

    }
}
