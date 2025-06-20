using MediatR;
using Microsoft.AspNetCore.Mvc;
using WalkForum.Application.Users.Commands.ConfirmAccount;
using WalkForum.Application.Users.Commands.Register;

namespace WalkForum.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterCommand command)
    {
        await mediator.Send(command);
        return Created();
    }

    [HttpGet("account-confirmation")]
    public async Task<IActionResult> ConfirmAccount([FromQuery] string token, [FromQuery] string email)
    {

        await mediator.Send(new ConfirmAccountCommand(email, token));
        return NoContent();
    }

}
