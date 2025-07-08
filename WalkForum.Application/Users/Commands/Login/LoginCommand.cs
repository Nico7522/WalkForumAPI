

using MediatR;

namespace WalkForum.Application.Users.Commands.Login;

public class LoginCommand : IRequest
{
    public required string Email { get; set; } 
    public required string Password { get; set; } 

}
