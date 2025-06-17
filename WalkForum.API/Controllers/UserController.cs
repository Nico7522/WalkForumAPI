using MediatR;
using Microsoft.AspNetCore.Mvc;
using WalkForum.Application.Users.Commands.AssignUserRole;
using WalkForum.Application.Users.Commands.UnassignUserRole;

namespace WalkForum.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IMediator mediator) : ControllerBase
    {
        [HttpPost("role")]
        public async Task<IActionResult> AssignRole(AssignUserRoleCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }
        [HttpDelete("role")]
        public async Task<IActionResult> UnassignRole(UnassignUserRoleCommand command)
        {
            await mediator.Send(command);
            return NoContent();

        }
    }
}

