using Microsoft.Extensions.DependencyInjection;
using WalkForum.Application.Posts;
using WalkForum.Domain.Repositories;

namespace WalkForum.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IPostsService, PostsService>();
    }
}
