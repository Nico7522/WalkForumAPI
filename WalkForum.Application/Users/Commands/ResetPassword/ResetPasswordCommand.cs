

using MediatR;
using System.Text.Json.Serialization;

namespace WalkForum.Application.Users.Commands.ResetPassword;

public class ResetPasswordCommand : IRequest
{
    public string Password { get; set; } = default!;
    public string PasswordConfirm { get; set; } = default!;

    [JsonIgnore]
    public string? Email { get; set; } 

    [JsonIgnore]
    public string? Token { get; set; } 

}
