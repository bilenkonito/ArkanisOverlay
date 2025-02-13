using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Windows;
using Windows.Win32;
using Application = System.Windows.Application;

namespace ArkanisOverlay;

/// <summary>
/// Interaction logic for Overlay.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        if (!Debugger.IsAttached)
        {
            var splashScreen = new SplashScreen("Resources\\ArkanisTransparent_512x512.png");
            splashScreen.Show(true, true);
        }
        
        base.OnStartup(e);
    }
}