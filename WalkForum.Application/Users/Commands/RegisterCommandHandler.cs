using MediatR;
using Microsoft.AspNetCore.Identity;
using WalkForum.Domain.Entities;

namespace WalkForum.Application.Users.Commands;

public class RegisterCommandHandler(IUserStore<User> userStore) : IRequestHandler<RegisterCommand>
{
    public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Email = request.Email,
            Surname = request.Surname,
            UserName = request.Username,
            Name = request.Name,
            
        };
        var result = await userStore.CreateAsync(user, cancellationToken);
        if (!result.Succeeded)
        {
            foreach (var err in result.Errors)
            {
                Console.WriteLine(err);
            }
        }
    }
}
