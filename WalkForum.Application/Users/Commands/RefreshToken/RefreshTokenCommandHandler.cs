

using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WalkForum.Application.Abstract;
using WalkForum.Domain.Entities;
using WalkForum.Domain.Exceptions;

namespace WalkForum.Application.Users.Commands.RefreshToken;

internal class RefreshTokenCommandHandler( 
    UserManager<User> userManager,
    SignInManager<User> signInManager,
    IHttpContextAccessor httpContextAccessor,
    IRefreshTokenGenerator refreshTokenGenerator) : IRequestHandler<RefreshTokenCommand>
{
    public async Task Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = httpContextAccessor.HttpContext?.Request.Cookies["refreshToken"];

        var user = await userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        if (user is null) throw new NotFoundException("User not found");


        if (string.IsNullOrEmpty(refreshToken) || user.RefreshToken != refreshToken || user.RefreshTokenExpiresAtDate < DateTime.Now)
            throw new BadRequestException("Invalid refresh token");

        await signInManager.SignInAsync(user, false);

        var resfreshToken = refreshTokenGenerator.GenerateRefreshToken();
        user.RefreshToken = resfreshToken;
        user.RefreshTokenExpiresAtDate = DateTime.UtcNow.AddDays(7);

        await userManager.UpdateAsync(user);

        httpContextAccessor.HttpContext?.Response.Cookies.Append("refreshToken", resfreshToken, new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddDays(7),
            Secure = true,
            SameSite = SameSiteMode.Strict
        });
    }
}
