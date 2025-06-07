using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using WalkForum.Application.Posts;
using WalkForum.Application.Users;
namespace WalkForum.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly));
        services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);
        services.AddValidatorsFromAssembly(typeof(ServiceCollectionExtensions).Assembly, includeInternalTypes: true);
        services.AddScoped<IUserContext, UserContext>();

        services.AddHttpContextAccessor();
    }
}
