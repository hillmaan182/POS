
using Application.ServiceHelper;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using POS;
using Tewr.Blazor.FileReader;
using Application.Service;
using Infrastructure.Helper;
using Infrastructure.AuthProvider;
using Blazored.LocalStorage;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7085/") });

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<AuthHeaderHandler>();

//Server Localhost
builder.Services.AddHttpClient("ServerAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:7085/");
})
.AddHttpMessageHandler<AuthHeaderHandler>();

builder.Services.AddScoped(sp =>
    sp.GetRequiredService<IHttpClientFactory>().CreateClient("ServerAPI"));

builder.Services.AddMudServices();
builder.Services.AddFileReaderService(o => o.UseWasmSharedBuffer = true);
builder.Services.AddSingleton<AppState>();

builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<IStringHelper,StringHelper>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
builder.Services.AddScoped<Application.IService.IAuthenticationService, Infrastructure.Service.AuthenticationService>();

await builder.Build().RunAsync();
