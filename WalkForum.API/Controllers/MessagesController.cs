using MediatR;
using Microsoft.AspNetCore.Mvc;
using WalkForum.Application.Messages.Commands.CreateMessage;
using WalkForum.Application.Messages.Commands.DeleteMessage;
using WalkForum.Application.Messages.Commands.UpdateMessage;
using WalkForum.Application.Messages.Dtos;
using WalkForum.Application.Messages.Queries.GetMessagesForPost;

namespace WalkForum.API.Controllers;

[Route("api/posts/{postId}/messages")]
[ApiController]
public class MessagesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MessageDto>>> GetAllForPost([FromRoute] int postId)
    {
        var messages = await mediator.Send(new GetMessagesForPostQuery(postId));
        return Ok(messages);
    }

    [HttpPost]
    public async Task<IActionResult> CreateMessage([FromRoute] int postId, CreateMessageCommand command)
    {
        command.PostId = postId;
        await mediator.Send(command);
        return Created();
    }

    [HttpDelete("{messageId}")]
    public async Task<IActionResult> DeleteMessageForPost([FromRoute] int postId, [FromRoute] int messageId)
    {
        await mediator.Send(new DeleteMessageForPostCommand(postId, messageId));
        return NoContent();
    }
    [HttpPatch("{messageId}")]
    public async Task<IActionResult> UpdateMessageForPost([FromRoute] int postId, [FromRoute] int messageId, [FromBody] UpdateMessageCommand command)
    {
        command.Id = messageId;
        command.PostId = postId;
        await mediator.Send(command);
        return NoContent();
    }
}
