

using MediatR;

namespace WalkForum.Application.Users.Commands.Login;

public class LoginCommand : IRequest
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public bool RememberMe { get; set; }

}
