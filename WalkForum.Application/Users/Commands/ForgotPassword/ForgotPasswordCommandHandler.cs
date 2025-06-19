

using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using WalkForum.Application.Utilities;
using WalkForum.Application.ViewModels;
using WalkForum.Domain.EmailModel;
using WalkForum.Domain.Entities;
using WalkForum.Domain.Exceptions;
using WalkForum.Domain.Repositories;

namespace WalkForum.Application.Users.Commands.ForgotPassword;

internal class ForgotPasswordCommandHandler(IEmailRepository emailRepository, 
    UserManager<User> userManager,
    IValidator<ForgotPasswordCommand> validator) : IRequestHandler<ForgotPasswordCommand>
{
    public async Task Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        Helpers.ValidForm(request, validator);
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user is null) throw new NotFoundException("User not found");

        var token = await userManager.GeneratePasswordResetTokenAsync(user);

        var param = new Dictionary<string, string?>
        {
            {"Token", token},
            {"Email", request.Email}
        };

        var callback = QueryHelpers.AddQueryString(request.ClientUri, param);

        EmailMetadata email = new(request.Email, "Reset your password");
        await emailRepository.SendUsingTemplate(email, new ForgotPasswordModel {Name = user.Name, Link = callback }, "ForgotPassword.cshtml");
    }
}
