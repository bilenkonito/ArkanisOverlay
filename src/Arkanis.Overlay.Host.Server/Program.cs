using System.Globalization;
using Arkanis.Overlay.Host.Server;
using Arkanis.Overlay.Host.Server.Components;
using Arkanis.Overlay.Infrastructure.Data;
using Arkanis.Overlay.Infrastructure.Data.Extensions;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(formatProvider: CultureInfo.InvariantCulture)
    .CreateBootstrapLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAllServerHostServices(builder.Configuration);

var app = builder.Build();

await app.MigrateDatabaseAsync<OverlayDbContext>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", true);
}

app.MapHealthChecks("/healthz");
app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
