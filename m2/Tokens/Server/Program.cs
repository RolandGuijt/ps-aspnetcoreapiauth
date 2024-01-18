using Microsoft.AspNetCore.Authentication.BearerToken;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddAuthentication()
    .AddBearerToken();
builder.Services.AddAuthorization();

var app = builder.Build();

app.MapGet("/login", (string username) =>
{
    var claimsPrincipal = new ClaimsPrincipal(
      new ClaimsIdentity(
        new[] { new Claim(ClaimTypes.Name, username) },
        BearerTokenDefaults.AuthenticationScheme
      )
    );

    return Results.SignIn(claimsPrincipal);
});

app.MapGet("/user", (ClaimsPrincipal user) =>
{
    return Results.Ok($"Welcome {user.Identity.Name}!");
}).RequireAuthorization();

app.Run();
