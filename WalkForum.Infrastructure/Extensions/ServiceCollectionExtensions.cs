using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using WalkForum.Domain.Repositories;
using WalkForum.Infrastructure.Persistance;
using WalkForum.Infrastructure.Repositories;
using WalkForum.Infrastructure.Seeders;
using WalkForum.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace WalkForum.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration) 
    {   
        var connectionString = configuration.GetSection("ConnectionString")["WalkForumDB"];
        services.AddDbContext<ForumDbContext>(options => options.UseNpgsql(connectionString).EnableSensitiveDataLogging());
        services.AddIdentityCore<User>().AddRoles<IdentityRole>().AddEntityFrameworkStores<ForumDbContext>().AddDefaultUI();

        services.AddScoped<ICategorySeeder, CategorySeeder>();
        services.AddScoped<ITagSeeder, TagSeeder>();
        services.AddScoped<IRoleSeeder, RoleSeeder>();

        services.AddScoped<IPostsRepository, PostsRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IMessagesRepository, MessagesRepository>();  
    }
}
