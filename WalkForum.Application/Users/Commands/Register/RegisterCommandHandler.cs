using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using WalkForum.Application.Emails;
using WalkForum.Application.Emails.EmailModel;
using WalkForum.Application.Emails.ViewModels;
using WalkForum.Application.Utilities;
using WalkForum.Domain.Constants;
using WalkForum.Domain.Entities;
using WalkForum.Domain.Exceptions;
namespace WalkForum.Application.Users.Commands.Register;

public class RegisterCommandHandler(UserManager<User> userManager, 
    IMapper mapper, 
    IValidator<RegisterCommand> validator,
    IConfiguration configuration,
    IEmailRepository emailRepository) : IRequestHandler<RegisterCommand>
{
    public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var userFindByEmail = await userManager.FindByEmailAsync(request.Email);
        if (userFindByEmail is not null) throw new BadRequestException("Email already taken");

        var userFindByUsername = await userManager.FindByNameAsync(request.Username);
        if (userFindByUsername is not null) throw new BadRequestException("Username already taken");

        await Helpers.ValidFormAsync<RegisterCommand>(request, validator);
        var userEntity = mapper.Map<User>(request);

        await userManager.CreateAsync(userEntity, request.Password);
        
        await userManager.AddToRoleAsync(userEntity, UserRoles.User);

        var token = await userManager.GenerateEmailConfirmationTokenAsync(userEntity);
        var param = new Dictionary<string, string?>
        {
            {
                "token", token
            },
            {
                "email", userEntity.Email
            }
        };

        var url = configuration["CallbackUrl"];
        if (String.IsNullOrWhiteSpace(url)) throw new Exception("Something went wrong");

        var callback = QueryHelpers.AddQueryString(url, param);

        EmailMetadata email = new(request.Email, "Confirm your account");
        await emailRepository.SendUsingTemplate(email, new ConfirmAccountModel { Name = userEntity.Name, Link = callback }, "ConfirmAccount.cshtml");

   
    }
}
