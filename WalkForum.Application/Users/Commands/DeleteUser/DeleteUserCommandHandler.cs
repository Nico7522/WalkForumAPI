
using MediatR;
using Microsoft.AspNetCore.Identity;
using WalkForum.Domain.Constants;
using WalkForum.Domain.Entities;
using WalkForum.Domain.Exceptions;

namespace WalkForum.Application.Users.Commands.DeleteUser;

internal class DeleteUserCommandHandler(UserManager<User> userManager,
    IUserContext userContext) : IRequestHandler<DeleteUserCommand>
{
    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        if (request.Id != userContext.GetCurrentUser().Id && !userContext.GetCurrentUser().IsInRole(UserRoles.Administrator)) throw new ForbiddenException("Not authorized");

        var user = await userManager.FindByIdAsync(request.Id.ToString());
        if (user is null) throw new NotFoundException("User not found");

        await userManager.DeleteAsync(user);
    }
}
