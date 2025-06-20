using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Web;
using WalkForum.Domain.Entities;
using WalkForum.Domain.Exceptions;


namespace WalkForum.Application.Users.Commands.ConfirmAccount;

internal class ConfirmAccountCommandHandler(UserManager<User> userManager) : IRequestHandler<ConfirmAccountCommand>
{
    public async Task Handle(ConfirmAccountCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user is null) throw new NotFoundException("User not found");

        var token = HttpUtility.UrlDecode(request.Token);

        var result = await userManager.ConfirmEmailAsync(user, token);

        if (!result.Succeeded) throw new BadRequestException("Invalid token");

    }
}
