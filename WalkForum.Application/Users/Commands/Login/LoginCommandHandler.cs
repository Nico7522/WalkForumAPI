using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using WalkForum.Application.Abstract;
using WalkForum.Application.Utilities;
using WalkForum.Domain.Entities;
using WalkForum.Domain.Exceptions;

namespace WalkForum.Application.Users.Commands.Login;

internal class LoginCommandHandler(UserManager<User> userManager,
    SignInManager<User> signInManager,
    IRefreshTokenGenerator refreshTokenGenerator,
    IHttpContextAccessor httpContextAccessor,
    IValidator<LoginCommand> validator) : IRequestHandler<LoginCommand>
{
    public async Task Handle(LoginCommand request, CancellationToken cancellationToken)
    {

        Helpers.ValidForm(request, validator);

        var user = await userManager.FindByEmailAsync(request.Email);
        if (user is null) throw new BadRequestException("Bad credentials");

        var isPasswordCorrect = await userManager.CheckPasswordAsync(user, request.Password);
        if (!isPasswordCorrect) throw new BadRequestException("Bad credentials");

        await signInManager.PasswordSignInAsync(user, request.Password, false, false);


        var resfreshToken = refreshTokenGenerator.GenerateRefreshToken();
        user.RefreshToken = resfreshToken;
        user.RefreshTokenExpiresAtDate = DateTime.UtcNow.AddDays(7);
        await userManager.UpdateAsync(user);

        httpContextAccessor.HttpContext!.Response.Cookies.Append("refreshToken", resfreshToken, new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddDays(7),
            Secure = true,
            SameSite = SameSiteMode.Strict
        });
    }
}
