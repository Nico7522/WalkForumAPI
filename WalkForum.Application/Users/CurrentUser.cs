
namespace WalkForum.Application.Users;

public record CurrentUser(string Id, string email, IEnumerable<string> Roles)
{
    public bool IsInRole(string role) => Roles.Contains(role);              
}
