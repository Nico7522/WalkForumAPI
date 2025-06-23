using MediatR;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace WalkForum.Application.UserProfile.Commands.UpdateAvatarUserProfile;

public class UpdateAvatarUserProfileCommand(int profileId, IFormFile file) : IRequest
{
    [JsonIgnore]
    public int ProfileId { get; set; } = profileId;
    public IFormFile File { get; set; } = file;
}
