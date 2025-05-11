namespace Arkanis.Overlay.Host.Desktop.Services;

using Microsoft.Extensions.Logging;
using Velopack;
using Velopack.Locators;
using Velopack.Sources;

public class ArkanisOverlayUpdateManager(IUpdateSource source, UpdateOptions? options = null, ILogger? logger = null, IVelopackLocator? locator = null)
    : UpdateManager(source, options, logger, locator)
{
    public string CurrentChannel
        => Channel;
}
