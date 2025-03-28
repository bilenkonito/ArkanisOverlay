using ArkanisOverlay.Data.Storage;
using ArkanisOverlay.Data.UEX.API;
using ArkanisOverlay.Helpers;
using ArkanisOverlay.Windows;
using ArkanisOverlay.Workers;
using Dapplo.Microsoft.Extensions.Hosting.AppServices;
using Dapplo.Microsoft.Extensions.Hosting.Wpf;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;

namespace ArkanisOverlay;

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
                options.MutexId = $"{{{Constants.INSTANCE_ID}}}";
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
                        => scope?.StartsWith("ArkanisOverlay") ?? false));

    private static IHostBuilder ConfigureServices(this IHostBuilder builder) =>
        builder.ConfigureServices(services =>
        {
            //* Add non-singleton Windows here
            // Example:
            // services.AddTransient<OtherWindow>();

            //? should be added automatically
            // IConfiguration configuration = new ConfigurationBuilder()
            //     .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            //     .AddJsonFile(
            //         $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json",
            //         optional: true, reloadOnChange: true)
            //     .AddEnvironmentVariables()
            //     .Build();

            // services.AddScoped<IConfiguration>(_ => configuration);


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
            services.AddSingleton<UEXContext>();

            // Services
            services.AddSingleton<BlurHelper>();
            services.AddSingleton<Client>();

            // Workers
            services.AddSingleton<WindowTracker>();
            services.AddSingleton<GlobalHotkey>();
            services.AddSingleton<DataSync>();
        });
}