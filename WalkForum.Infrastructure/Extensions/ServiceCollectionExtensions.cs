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

namespace WalkForum.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration) 
    {   
        var connectionString = configuration.GetSection("ConnectionString")["WalkForumDB"];
        services.AddDbContext<ForumDbContext>(options => options.UseNpgsql(connectionString).EnableSensitiveDataLogging());
        services.AddIdentityApiEndpoints<User>(options =>
        {
            options.User.RequireUniqueEmail = true;

        }).AddRoles<IdentityRole<int>>().AddClaimsPrincipalFactory<WalkForumUserClaimsPrincipalFactory>().AddEntityFrameworkStores<ForumDbContext>().AddSignInManager<CustomSignInManager>();
        //services.AddScoped<SignInManager<User>, CustomSignInManager>();

        services.AddScoped<ICategorySeeder, CategorySeeder>();
        services.AddScoped<ITagSeeder, TagSeeder>();
        services.AddScoped<IRoleSeeder, RoleSeeder>();

        services.AddScoped<IPostsRepository, PostsRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IMessagesRepository, MessagesRepository>();  
    }
}
