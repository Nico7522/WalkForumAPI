using WalkForum.Domain.Entities;

namespace WalkForum.Application.Users.Dtos;

public class UserDto
{
    public int Id { get; set; }
    public string UserName { get; set; } = default!;
  
}
