﻿
using MediatR;

namespace WalkForum.Application.Users.Commands.AssignUserRole;

public class AssignUserRoleCommand : IRequest
{
    public string Email { get; set; } = default!;
    public string Role { get; set; } = default!;
}
