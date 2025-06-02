using WalkForum.Domain.Entities;

namespace WalkForum.Application.Users;

public class UserDto
{
    public int Id { get; set; }
    public string Username { get; set; } = default!;

    public static UserDto FromEntity(User user)
    {
        return new UserDto() { Id = user.Id, Username = user.Username };
    }
}
