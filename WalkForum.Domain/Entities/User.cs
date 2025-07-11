﻿namespace WalkForum.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Surname { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;

    public ICollection<Post> Posts { get; set; } = new List<Post>();
    public ICollection<Message> Messages { get; set; } = new List<Message>();

}
