using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TaskManager.UI;
using TaskManager.UI.Infrastructure.Auth;
using TaskManager.UI.Infrastructure.Cards;
using TaskManager.UI.Infrastructure.User;
using TaskManager.UI.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICardService, CardService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped(sp =>
{
    var client = new HttpClient
    {
        BaseAddress = new Uri("https://localhost:8081/")
    };
    
    return client;
});

builder.Services.AddScoped<AuthorizedHttpClient>();
builder.Services.AddScoped<CustomAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp => 
    sp.GetRequiredService<CustomAuthStateProvider>());
builder.Services.AddAuthorizationCore(); 


builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();