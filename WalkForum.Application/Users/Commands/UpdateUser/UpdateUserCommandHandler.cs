using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using WalkForum.Domain.Constants;
using WalkForum.Domain.Entities;
using WalkForum.Domain.Exceptions;

namespace WalkForum.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler(UserManager<User> userManager, 
    IMapper mapper,
    IUserContext userContext) : IRequestHandler<UpdateUserCommand>
{
    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        if (request.Id != userContext.GetCurrentUser().Id && !userContext.GetCurrentUser().IsInRole(UserRoles.Administrator)) throw new ForbiddenException("Not authorized");

        var user = await userManager.FindByIdAsync(request.Id.ToString());
        if (user is null) throw new NotFoundException("User not found");


        mapper.Map(request, user);

        await userManager.UpdateAsync(user);
    }
}
