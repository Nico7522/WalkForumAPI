using Microsoft.AspNetCore.Http.Features;
using Serilog;
using WalkForum.API.Middlewares;
using WalkForum.Application.Extensions;
using WalkForum.Infrastructure.Extensions;
using WalkForum.Infrastructure.Seeders;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});
builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = context =>
    {
        context.ProblemDetails.Instance = $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";
        context.ProblemDetails.Extensions.TryAdd("requestId",context.HttpContext.TraceIdentifier);

        var activity = context.HttpContext.Features.Get<IHttpActivityFeature>()?.Activity;
        context.ProblemDetails.Extensions.TryAdd("TraceId", activity?.Id);
    };  
});
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
var app = builder.Build();

var scope = app.Services.CreateScope();
var Categoryseeder = scope.ServiceProvider.GetRequiredService<ICategorySeeder>();
var TagSeeder = scope.ServiceProvider.GetRequiredService<ITagSeeder>();

await Categoryseeder.Seed();
await TagSeeder.Seed();

app.UseSwagger();
app.UseSwaggerUI();


app.UseSerilogRequestLogging();
app.UseExceptionHandler();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
