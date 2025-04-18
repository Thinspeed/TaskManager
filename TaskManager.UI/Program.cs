using Blazored.Toast;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TaskManager.UI;
using TaskManager.UI.Infrastructure.Auth;
using TaskManager.UI.Infrastructure.Cards;
using TaskManager.UI.Infrastructure.Comments;
using TaskManager.UI.Infrastructure.User;
using TaskManager.UI.Services.Auth;
using TaskManager.UI.Services.Cards;
using TaskManager.UI.Services.Comments;
using TaskManager.UI.Services.Common;
using TaskManager.UI.Services.Users;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICardService, CardService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped(sp =>
{
    var client = new HttpClient
    {
        BaseAddress = builder.HostEnvironment.IsProduction() 
            ? new Uri($"{builder.HostEnvironment.BaseAddress}api") 
            : new Uri("http://localhost:5000")
    };
    
    return client;
});

builder.Services.AddScoped<AuthorizedHttpClient>();
builder.Services.AddScoped<CustomAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp => 
    sp.GetRequiredService<CustomAuthStateProvider>());
builder.Services.AddAuthorizationCore(); 

builder.Services.AddBlazoredToast();

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

await builder.Build().RunAsync();