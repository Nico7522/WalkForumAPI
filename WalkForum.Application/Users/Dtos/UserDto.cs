using WalkForum.Domain.Entities;

namespace WalkForum.Application.Users.Dtos;

public class UserDto
{
    public int Id { get; set; }
    public string Username { get; set; } = default!;
  
}
