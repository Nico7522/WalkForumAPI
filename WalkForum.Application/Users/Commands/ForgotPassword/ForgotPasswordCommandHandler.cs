

using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using WalkForum.Application.Utilities;
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

        EmailMetadata email = new(request.Email, "Reset your password");
        var template = $"{Directory.GetCurrentDirectory()}/EmailTemplates/ForgotPassword.cshtml";
        await emailRepository.SendUsingTemplate(email, user.Name, template);
    }
}
