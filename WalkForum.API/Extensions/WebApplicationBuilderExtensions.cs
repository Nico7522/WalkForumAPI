using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Diagnostics;
using WalkForum.API.Middlewares;


namespace WalkForum.API.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddPresentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication().AddCookie();

        // Uncomment the following lines if you want to use cookie authentication and use .AddIdentity insteand of .AddIdentityApiEnpoints
        //builder.Services.AddAuthentication(options =>
        //{
        //    options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
        //    options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
        //    options.DefaultSignInScheme = IdentityConstants.ApplicationScheme;
        //})
        //.AddCookie("Identity.Bearer", options =>
        //{
        //    options.Cookie.Name = "jwt";
        //    options.LoginPath = "/login";
        //    options.SlidingExpiration = false;
        //    options.ExpireTimeSpan = TimeSpan.FromHours(1);
        //    options.Cookie.HttpOnly = true;
        //    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        //    options.Cookie.IsEssential = true;
        //    options.Cookie.SameSite = SameSiteMode.Strict;
        //    options.Cookie.MaxAge = TimeSpan.FromDays(30);
        //});

        builder.Services.TryAddSingleton(TimeProvider.System);
        builder.Services.AddControllers();

        builder.Services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme { Type = SecuritySchemeType.Http, Scheme = "Bearer" });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference {Type = ReferenceType.SecurityScheme, Id = "bearerAuth" }
            },
            []
        }
    });
        });
        builder.Services.AddProblemDetails(options =>
        {
            options.CustomizeProblemDetails = context =>
            {
                context.ProblemDetails.Instance = $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";

                context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);

                Activity? activity = context.HttpContext.Features.Get<IHttpActivityFeature>()?.Activity;
                context.ProblemDetails.Extensions.TryAdd("traceId", activity?.Id);

            };
        });
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Host.UseSerilog((context, configuration) =>
        {
            configuration.ReadFrom.Configuration(context.Configuration);
        });

    }
}
