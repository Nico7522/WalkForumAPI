using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
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

        var userFoundById = await userManager.FindByIdAsync(request.Id.ToString());
        if (userFoundById is null) throw new NotFoundException("User not found");

        if (userFoundById.UserName != request.Username)
        {
            var userFoundByUsername = await userManager.FindByNameAsync(request.Username);
            if (userFoundByUsername is null) throw new NotFoundException("User not found");
        }

        mapper.Map(request, userFoundById);
        await userManager.UpdateAsync(userFoundById);
    }
}
