using Serilog;
using System.Text.Json.Serialization;
using System.Text.Json;
using WalkForum.API.Middlewares;
using WalkForum.Application.Extensions;
using WalkForum.Infrastructure.Extensions;
using WalkForum.Infrastructure.Seeders;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddScoped<ErrorHandlingMiddleware>();
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
var app = builder.Build();

var scope = app.Services.CreateScope();
var Categoryseeder = scope.ServiceProvider.GetRequiredService<ICategorySeeder>();
var TagSeeder = scope.ServiceProvider.GetRequiredService<ITagSeeder>();

await Categoryseeder.Seed();
await TagSeeder.Seed();

app.UseSwagger();
app.UseSwaggerUI();


app.UseSerilogRequestLogging();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
