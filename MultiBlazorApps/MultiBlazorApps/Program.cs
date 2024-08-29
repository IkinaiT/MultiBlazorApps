using MultiBlazorApps.Client.Pages;
using MultiBlazorApps.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
builder.Services.AddRouting();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

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

app.MapWhen(ctx => ctx.Request.Path.StartsWithSegments("/FirstClient", StringComparison.OrdinalIgnoreCase), first =>
{
    first.UseBlazorFrameworkFiles("/FirstClient");

    first.UseStaticFiles();
    first.UseStaticFiles("/FirstClient");

    first.UseRouting();
    first.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
        endpoints.MapFallbackToFile("/FirstClient/{*path:nonfile}", "FirstClient/index.html");
    });
});

app.MapWhen(ctx => ctx.Request.Path.StartsWithSegments("/SecondClient", StringComparison.OrdinalIgnoreCase), second =>
{
    second.UseBlazorFrameworkFiles("/SecondClient");

    second.UseStaticFiles();
    second.UseStaticFiles("/SecondClient");

    second.UseRouting();
    second.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
        endpoints.MapFallbackToFile("/SecondClient/{*path:nonfile}", "SecondClient/index.html");
    });
});

app.MapWhen(ctx => ctx.Request.Path.StartsWithSegments("/ThirdClient", StringComparison.OrdinalIgnoreCase), third =>
{
    third.UseBlazorFrameworkFiles("/ThirdClient");

    third.UseStaticFiles();
    third.UseStaticFiles("/ThirdClient");

    third.UseRouting();
    third.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
        endpoints.MapFallbackToFile("/ThirdClient/{*path:nonfile}", "ThirdClient/index.html");
    });
});

app.MapWhen(ctx => ctx.Request.Path.StartsWithSegments("/FourthClient", StringComparison.OrdinalIgnoreCase), fourth =>
{
    fourth.UseBlazorFrameworkFiles("/FourthClient");

    fourth.UseStaticFiles();
    fourth.UseStaticFiles("/FourthClient");

    fourth.UseRouting();
    fourth.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
        endpoints.MapFallbackToFile("/FourthClient/{*path:nonfile}", "FourthClient");
    });
});

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(MultiBlazorApps.Client._Imports).Assembly);

app.Run();
