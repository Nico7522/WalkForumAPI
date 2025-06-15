

using MediatR;
using Microsoft.AspNetCore.Identity;
using WalkForum.Domain.Entities;
using WalkForum.Domain.Exceptions;

namespace WalkForum.Application.Users.Commands.AssignUserRole;

internal class AssignUserRoleCommandHandler(UserManager<User> userManager,
    RoleManager<IdentityRole<int>> roleManager) : IRequestHandler<AssignUserRoleCommand>
{
    public async Task Handle(AssignUserRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user is null) throw new NotFoundException("User not found");

        var role = await roleManager.FindByNameAsync(request.Role);
        if (role is null) throw new NotFoundException("Role not found");

        await userManager.AddToRoleAsync(user, role.Name!);

    }
}
