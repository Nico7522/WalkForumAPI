using MediatR;

namespace WalkForum.Application.Users.Commands.Register;

public class RegisterCommand(string name, string surname, string username, string email, string password, string passwordConfirm) : IRequest
{
    public string Name { get; set; } = name;
    public string Surname { get; set; } = surname;
    public string Email { get; set; } = email;
    public string Username { get; set; } = username;
    public string Password { get; set; } = password;
    public string PasswordConfirm { get; set; } = passwordConfirm;
}
