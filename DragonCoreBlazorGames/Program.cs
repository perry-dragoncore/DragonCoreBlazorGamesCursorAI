using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using DragonCoreBlazorGames;
using Microsoft.Fast.Components.FluentUI;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
// SignalR is not available in Blazor WebAssembly, so we need to remove this line
//builder.Services.AddSignalR(); // This line is commented out as it's not applicable

builder.Services.AddFluentUIComponents();

var app = builder.Build();

//app.MapHub<TicTacToeHub>("/tictactoehub");

await app.RunAsync();
