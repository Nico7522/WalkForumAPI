

using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using WalkForum.Domain.Entities;
using WalkForum.Domain.Exceptions;

namespace WalkForum.Application.Users.Commands.Login;

internal class LoginCommandHandler(UserManager<User> userManager, SignInManager<User> signInManager) : IRequestHandler<LoginCommand>
{
    public async Task Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user is null) throw new BadRequestException("Bad credentials");

        var isPasswordCorrect = await userManager.CheckPasswordAsync(user, request.Password);
        if (!isPasswordCorrect) throw new BadRequestException("Bad credentials");

        await signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, false);

        if (request.RememberMe)
        {
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(3)
            };
            await signInManager.SignInAsync(user, authProperties);

        }
    }
}
