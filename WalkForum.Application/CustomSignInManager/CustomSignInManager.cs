using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WalkForum.Domain.Entities;
using WalkForum.Domain.Exceptions;


namespace WalkForum.Application.CustomSignInManager
{
    public class CustomSignInManager : SignInManager<User>
    {
        private readonly UserManager<User> _userManager;

        public CustomSignInManager(UserManager<User> userManager,
                                  IHttpContextAccessor contextAccessor,
                                  IUserClaimsPrincipalFactory<User> claimsFactory,
                                  IOptions<IdentityOptions> optionsAccessor,
                                  ILogger<SignInManager<User>> logger,
                                  IAuthenticationSchemeProvider schemes,
                                  IUserConfirmation<User> confirmation)
            : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {
            _userManager = userManager;
        }

        public override async Task<SignInResult> PasswordSignInAsync(string email, string password, bool isPersistent, bool lockoutOnFailure)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is  null) throw new BadRequestException("Bad credentials");

            var result = await base.PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);
            if(!result.Succeeded) throw new BadRequestException("Bad credentials");

            return result;
        }
    }
}
