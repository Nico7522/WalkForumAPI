using MediatR;
using Microsoft.AspNetCore.Mvc;
using WalkForum.Application.Users.Commands;

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

}
