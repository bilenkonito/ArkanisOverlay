using System.Globalization;
using Arkanis.Overlay.Common.Abstractions;
using Arkanis.Overlay.Common.Services;
using Arkanis.Overlay.Components.Helpers;
using Arkanis.Overlay.Components.Services;
using Arkanis.Overlay.Host.Server.Components;
using Arkanis.Overlay.Host.Server.Services;
using Arkanis.Overlay.Infrastructure;
using Arkanis.Overlay.Infrastructure.Data;
using Arkanis.Overlay.Infrastructure.Data.Extensions;
using Arkanis.Overlay.Infrastructure.Services;
using Arkanis.Overlay.Infrastructure.Services.Abstractions;
using MudBlazor;
using MudBlazor.Services;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(formatProvider: CultureInfo.InvariantCulture)
    .CreateBootstrapLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSerilog(loggerConfig => loggerConfig
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(builder.Configuration)
);

builder.Services.AddLogging();
builder.Services.AddHttpClient();

builder.Services.AddMemoryCache();

builder.Services
    .AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices(options =>
    {
        options.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopCenter;
        options.SnackbarConfiguration.PreventDuplicates = false;
        options.SnackbarConfiguration.ClearAfterNavigation = false;
    }
);

builder.Services
    .AddJavaScriptEventInterop()
    .AddGlobalKeyboardProxyService()
    .AddGoogleTrackingServices()
    .AddSingleton<SharedAnalyticsPropertyProvider, ServerAnalyticsPropertyProvider>()
    .AddServerOverlayControls()
    .AddInfrastructure()
    .AddInfrastructureConfiguration(builder.Configuration)
    .AddSingleton<GitHubReleasesService>()
    .AddSingleton<IAppVersionProvider, AssemblyAppVersionProvider>()
    .AddSingleton<ISystemAutoStartStateProvider, NoSystemAutoStartStateProvider>();

var app = builder.Build();

await app.MigrateDatabaseAsync<OverlayDbContext>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", true);
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
