

using MediatR;

namespace WalkForum.Application.Users.Commands.ForgotPassword;

public class ForgotPasswordCommand : IRequest
{
    public string Email { get; set; } = default!; 
}
