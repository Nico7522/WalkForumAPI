using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Web;
using WalkForum.Application.Utilities;
using WalkForum.Domain.Entities;
using WalkForum.Domain.Exceptions;

namespace WalkForum.Application.Users.Commands.ResetPassword;

internal class ResetPasswordCommandHandler(UserManager<User> userManager, IValidator<ResetPasswordCommand> validator) : IRequestHandler<ResetPasswordCommand>
{
    public async Task Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        Helpers.ValidForm(request, validator);
        var user = await userManager.FindByEmailAsync(request.Email!);
        if (user is null) throw new NotFoundException("User not found");

        var token = HttpUtility.UrlDecode(request.Token);

        var result = await userManager.ResetPasswordAsync(user, token!, request.Password);
        if (!result.Succeeded) throw new BadRequestException("Invalid token");
    }
}
