using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WalkForum.Application.Users.Commands.AssignUserRole;
using WalkForum.Application.Users.Commands.ForgotPassword;
using WalkForum.Application.Users.Commands.ResetPassword;
using WalkForum.Application.Users.Commands.UnassignUserRole;
using WalkForum.Application.Users.Commands.UpdateUser;
using WalkForum.Domain.Constants;

namespace WalkForum.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IMediator mediator) : ControllerBase
    {
        [HttpPost("role")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = UserRoles.Administrator)]
        public async Task<IActionResult> AssignRole(AssignUserRoleCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }
        [HttpDelete("role")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = UserRoles.Administrator)]
        public async Task<IActionResult> UnassignRole(UnassignUserRoleCommand command)
        {
            await mediator.Send(command);
            return NoContent();

        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] UpdateUserCommand command, [FromRoute] int id)
        {
            command.Id = id;
            await mediator.Send(command);
            return NoContent();
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }
        [HttpPost("reset-password/{token}")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command, [FromQuery] string token, [FromQuery] string email)
        {
            command.Token = token;
            command.Email = email;
            await mediator.Send(command);
            return NoContent();
        }
    }
}

