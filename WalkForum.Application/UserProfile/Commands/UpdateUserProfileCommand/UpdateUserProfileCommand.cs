using MediatR;
using System.Text.Json.Serialization;

namespace WalkForum.Application.UserProfile.Commands.UpdateUserProfileCommand;

public class UpdateUserProfileCommand : IRequest
{
    [JsonIgnore]
    public int ProfileId { get; set; }

    public string? Presentation { get; set; }

}
