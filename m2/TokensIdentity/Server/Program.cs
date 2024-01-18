using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddAuthentication()
    .AddBearerToken(IdentityConstants.BearerScheme);
builder.Services.AddAuthorizationBuilder();

builder.Services.AddDbContext<AppDbContext>(o => 
    o.UseSqlite("DataSource=app.db"));

builder.Services.AddIdentityCore<IdentityUser>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddApiEndpoints();

var app = builder.Build();

app.MapIdentityApi<IdentityUser>();

app.MapGet("/user", (ClaimsPrincipal user) =>
{
    return Results.Ok($"Welcome {user.Identity.Name}!");
}).RequireAuthorization();

app.Run();

class AppDbContext : IdentityDbContext {
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }
}