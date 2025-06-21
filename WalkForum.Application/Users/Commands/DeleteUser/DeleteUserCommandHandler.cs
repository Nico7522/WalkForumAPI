
using MediatR;
using Microsoft.AspNetCore.Identity;
using WalkForum.Domain.Entities;
using WalkForum.Domain.Exceptions;

namespace WalkForum.Application.Users.Commands.DeleteUser;

internal class DeleteUserCommandHandler(UserManager<User> userManager) : IRequestHandler<DeleteUserCommand>
{
    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.Id.ToString());
        if (user is null) throw new NotFoundException("User not found");

        await userManager.DeleteAsync(user);
    }
}
