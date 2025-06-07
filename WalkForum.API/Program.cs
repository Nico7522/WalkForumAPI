using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using Serilog;
using WalkForum.API.Extensions;
using WalkForum.API.Middlewares;
using WalkForum.Application.Extensions;
using WalkForum.Domain.Entities;
using WalkForum.Infrastructure.Extensions;
using WalkForum.Infrastructure.Seeders;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.AddPresentation();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

var scope = app.Services.CreateScope();
var Categoryseeder = scope.ServiceProvider.GetRequiredService<ICategorySeeder>();
var TagSeeder = scope.ServiceProvider.GetRequiredService<ITagSeeder>();
var RoleSeeder = scope.ServiceProvider.GetRequiredService<IRoleSeeder>();


await Categoryseeder.Seed();
await TagSeeder.Seed();
await RoleSeeder.Seed();

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

app.MapGroup("api/identity").MapIdentityApi<User>();

app.UseAuthorization();

app.MapControllers();

app.Run();
