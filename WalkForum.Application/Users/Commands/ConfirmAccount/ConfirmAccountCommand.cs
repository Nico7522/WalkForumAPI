

using MediatR;

namespace WalkForum.Application.Users.Commands.ConfirmAccount;

public class ConfirmAccountCommand(string email, string token) : IRequest
{
    public string Email { get; } = email;
    public string Token { get; } = token;

}
