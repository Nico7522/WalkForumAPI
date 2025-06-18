using MediatR;
using System.Text.Json.Serialization;

namespace WalkForum.Application.Users.Commands.UpdateUser;

public class UpdateUserCommand : IRequest
{
    [JsonIgnore]
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Surname { get; set; } = default!;
    public string Username { get; set; } = default!;


}
