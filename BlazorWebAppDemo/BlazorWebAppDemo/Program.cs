using Blazored.LocalStorage;
using BlazorWebAppDemo.Client.Pages;
using BlazorWebAppDemo.Components;
using BlazorWebAppDemo.Components.StateService;
using BlazorWebAppDemo.Services;
using Microsoft.AspNetCore.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddSingleton<IMyService, MyService>();

builder.Services.AddCascadingValue(sp =>
{
    var model = new CascadingModel();
    model.SomeText = "Hello from CascadingValue";
    var source = new CascadingValueSource<CascadingModel>(model, isFixed: false);
    model.PropertyChanged += (sender, args) => source.NotifyChangedAsync();
    return source;
});

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddSingleton<StateService>();
builder.Services.AddSingleton<SuperheroData>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BlazorWebAppDemo.Client._Imports).Assembly);

app.Run();
