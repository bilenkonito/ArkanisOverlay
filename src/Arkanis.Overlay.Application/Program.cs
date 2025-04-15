using Arkanis.Overlay.Application.Data.API;
using Arkanis.Overlay.Application.Data.Contexts;
using Arkanis.Overlay.Application.Data.Options;
using Arkanis.Overlay.Application.Helpers;
using Arkanis.Overlay.Application.Services;
using Arkanis.Overlay.Application.UI;
using Arkanis.Overlay.Application.UI.Windows;
using Arkanis.Overlay.Application.Workers;
using Dapplo.Microsoft.Extensions.Hosting.AppServices;
using Dapplo.Microsoft.Extensions.Hosting.Wpf;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;

namespace Arkanis.Overlay.Application;

// based on:
// https://github.com/dapplo/Dapplo.Microsoft.Extensions.Hosting/blob/master/samples/Dapplo.Hosting.Sample.WpfDemo/Program.cs#L48
public static class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureLogging()
            .ConfigureSingleInstance(options =>
            {
                options.MutexId = $"{{{Constants.InstanceId}}}";
                options.WhenNotFirstInstance = (environment, logger) =>
                {
                    logger.LogInformation("{appName} is already running.", environment.ApplicationName);
                };
            })
            //? add plugin support later to support modular add-ons?
            // .ConfigurePlugins()
            .ConfigureServices()
            .ConfigureWpf(options =>
            {
                options.UseApplication<App>();
                
                //* Add Singleton Windows here
                // Windows will be registered as singletons
                options.UseWindow<OverlayWindow>();
            })
            .UseWpfLifetime()
            .UseConsoleLifetime()
            .Build();

        host.Run();
    }

    private static IHostBuilder ConfigureLogging(this IHostBuilder hostBuilder) =>
        hostBuilder.ConfigureLogging((hostContext, configLogging) =>
            configLogging
                .AddConfiguration(hostContext.Configuration.GetSection("Logging"))
                .AddConsole()
                .AddDebug()
                .SetMinimumLevel(LogLevel.Debug)
                .AddFilter(
                    (scope, _)
                        => scope?.StartsWith("Arkanis") ?? false));

    private static IHostBuilder ConfigureServices(this IHostBuilder builder)
    {
        return builder.ConfigureServices((context, services) =>
        {
            //* Add non-singleton Windows here
            // Example:
            // services.AddTransient<OtherWindow>();

            services
                .AddOptions<ConfigurationOptions>()
                .BindConfiguration(ConfigurationOptions.Section)
                .ValidateDataAnnotations()
                .ValidateOnStart();

            services.AddWpfBlazorWebView();
            services.AddMudServices(config =>
            {
                config.SnackbarConfiguration.NewestOnTop = true;
                config.SnackbarConfiguration.MaxDisplayedSnackbars = 1;
            });
            services.AddSingleton<IServiceProvider>(sp => sp);
            services.AddHttpClient();

            // Data
            services.AddScoped<UEXContext>();

            // Hosted Services
            services.AddHostedService<EndpointManager>();

            // Singleton Services
            services.AddSingleton<BlurHelper>();
            services.AddSingleton<DataClient>();
            services.AddMemoryCache();

            // Scoped Services
            services.AddScoped<ISearchService, SearchService>();

            // Workers
            services.AddSingleton<WindowTracker>();
            services.AddSingleton<GlobalHotkey>();
            services.AddSingleton<DataSync>();
        });
    }
}