using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WalkForum.Domain.Repositories;
using WalkForum.Infrastructure.Persistance;
using WalkForum.Infrastructure.Repositories;
using WalkForum.Infrastructure.Seeders;
using WalkForum.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using WalkForum.Infrastructure.Authorization;
using WalkForum.Application.CustomSignInManager;
using WalkForum.Application.Emails;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Http;
using WalkForum.Infrastructure.Security;
using WalkForum.Application.Abstract;
using WalkForum.Infrastructure.Authorization.Services;
using WalkForum.Domain.AuthorizationInterfaces;


namespace WalkForum.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration) 
    {   
        var connectionString = configuration.GetSection("ConnectionString")["WalkForumDB"];
        services.AddDbContext<ForumDbContext>(options => options.UseNpgsql(connectionString).EnableSensitiveDataLogging());
        services.AddIdentity<User, IdentityRole<int>>(options =>
        {
            options.User.RequireUniqueEmail = true;

        }).AddRoles<IdentityRole<int>>()
        .AddClaimsPrincipalFactory<WalkForumUserClaimsPrincipalFactory>()
        .AddEntityFrameworkStores<ForumDbContext>()
        .AddSignInManager<CustomSignInManager>()
        .AddDefaultTokenProviders();
        services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.Name = "authorization";
            options.LoginPath = "/login";
            options.SlidingExpiration = false;
            options.ExpireTimeSpan = TimeSpan.FromHours(1);
            options.Cookie.HttpOnly = true;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            options.Cookie.IsEssential = true;
            options.Cookie.SameSite = SameSiteMode.Strict;
            options.Events.OnRedirectToLogin = context =>
            {
                context.Response.StatusCode = 401; 
                return Task.CompletedTask;
            };

            options.Events.OnRedirectToAccessDenied = context =>
            {
                context.Response.StatusCode = 403; 
                return Task.CompletedTask;
            };
        });
        services.Configure<DataProtectionTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromHours(1));

        services.AddScoped<ICategorySeeder, CategorySeeder>();
        services.AddScoped<ITagSeeder, TagSeeder>();
        services.AddScoped<IRoleSeeder, RoleSeeder>();
        services.AddScoped<IPostsRepository, PostsRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ITagsRepository, TagsRepository>();
        services.AddScoped<IMessagesRepository, MessagesRepository>();
        services.AddScoped<IUserProfileRepository, UserProfileRepository>();
        services.AddScoped<IPrivateDiscussionRepository, PrivateDiscussionRepository>();
        services.AddScoped<IPrivateMessageRepository, PrivateMessageRepository>();
        services.AddScoped<IRefreshTokenGenerator, RefreshTokenGenerator>();
        services.AddScoped<IPostAuthorizationService, PostAuthorizationService>();



        var emailSettings = configuration.GetSection("EmailSettings");
        var defaultFromEmail = emailSettings["DefaultFromEmail"];
        var host = emailSettings["SMTPSetting:Host"];
        var port = emailSettings.GetValue<int>("Port");
        var username = emailSettings["username"];
        var password = emailSettings["password"];
        services.AddFluentEmail(defaultFromEmail).AddSmtpSender(host, port, username, password).AddRazorRenderer();
        services.AddScoped<IEmailRepository, EmailRepository>();



    }
}
