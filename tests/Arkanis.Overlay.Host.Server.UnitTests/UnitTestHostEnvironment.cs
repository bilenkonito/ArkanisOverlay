namespace Arkanis.Overlay.Host.Server.UnitTests;

using Common;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

public class UnitTestHostEnvironment : IHostEnvironment
{
    public string EnvironmentName { get; set; } = "Test";
    public string ApplicationName { get; set; } = ApplicationConstants.ApplicationName;
    public string ContentRootPath { get; set; } = Path.GetTempPath();
    public IFileProvider ContentRootFileProvider { get; set; } = new CompositeFileProvider();
}
