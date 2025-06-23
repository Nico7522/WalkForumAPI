using MediatR;
using Microsoft.AspNetCore.Mvc;
using WalkForum.Application.UserProfile.Commands.UpdateAvatarUserProfile;
using WalkForum.Application.UserProfile.Commands.UpdateUserProfileCommand;

namespace WalkForum.API.Controllers
{
    [Route("api/user/profile")]
    [ApiController]
    public class ProfileController(IMediator mediator) : ControllerBase
    {
        [HttpPatch("{profileId}")]
        public async Task<IActionResult> Update([FromRoute] int profileId, [FromBody] UpdateUserProfileCommand command)
        {
            command.ProfileId = profileId;    
            await mediator.Send(command);   
            return NoContent();
        }

        [HttpPatch("{profileId}/avatar")]
        public async Task<IActionResult> UpdateAvatar([FromRoute] int profileId, IFormFile file)
        {
           
            await mediator.Send(new UpdateAvatarUserProfileCommand(profileId, file));
            return NoContent();
        }
    }
}
