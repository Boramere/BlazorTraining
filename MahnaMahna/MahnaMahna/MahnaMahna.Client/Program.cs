using MahnaMahna.Client.Services;
using MahnaMahna.Shared.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

//Services
builder.Services.AddScoped<ITodoApiService, TodoApiService>();
builder.Services.AddHttpClient("", (HttpClient client) => { client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress); });

//SignalR
builder.Services.AddScoped<INotificationService, NotificationService>();

await builder.Build().RunAsync();
