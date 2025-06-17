
using MediatR;

namespace WalkForum.Application.Users.Commands.UnassignUserRole;

public class UnassignUserRoleCommand : IRequest
{
    public string Email { get; set; } = default!;
    public string Role { get; set; } = default!;
}
