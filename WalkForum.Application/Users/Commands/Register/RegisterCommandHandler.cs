using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using WalkForum.Domain.Constants;
using WalkForum.Domain.Entities;
using WalkForum.Domain.Exceptions;
namespace WalkForum.Application.Users.Commands.Register;

public class RegisterCommandHandler(UserManager<User> userManager, IMapper mapper, IValidator<RegisterCommand> validator) : IRequestHandler<RegisterCommand>
{
    public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        await Helpers.Helpers.ValidFormAsync<RegisterCommand>(request, validator);
        var userEntity = mapper.Map<User>(request);

        userEntity.UserProfile = new UserProfile() { CreationDate = DateTime.Now, UpdateDate = DateTime.Now };
        await userManager.CreateAsync(userEntity, request.Password);
        
        await userManager.AddToRoleAsync(userEntity, UserRoles.User);

   
    }
}
