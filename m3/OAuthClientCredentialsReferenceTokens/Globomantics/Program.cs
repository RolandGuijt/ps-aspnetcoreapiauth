using Globomantics;
using Globomantics.ApiServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddScoped<IConferenceApiService, ConferenceApiService>();
builder.Services.AddScoped<IProposalApiService, ProposalApiService>();
builder.Services.AddScoped<EnsureAccessTokenFilter>();

builder.Services.AddSingleton(sp =>
{
    var client = new HttpClient { BaseAddress = new Uri("https://localhost:5002") };
    return client;
});

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Conference}/{action=Index}/{id?}");


app.Run();
